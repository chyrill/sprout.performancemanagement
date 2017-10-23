using al.performancemanagement.BOL.BO;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace al.performancemanagement.SchedulerService
{
    [ServiceBehavior(Namespace = "al.performancemanagement.SchedulerService", Name = "SchedulerService", InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class SchedulerService
    {
        private static SchedulerService m_instance;
        private bool m_isActive;

        private EmployeeReviewBO m_EmployeeReviewBO = new EmployeeReviewBO();

        public static SchedulerService GetService()
        {
            if (m_instance == null)
            {
                m_instance = new SchedulerService();
            }

            return m_instance;
        }

        public bool Start()
        {
            this.StartScheduler();
            return true;
        }

        public bool Stop()
        {
            m_isActive = false;
            return m_isActive;
        }

        private SchedulerService()
        {
            m_instance = this;
            this.StartScheduler();
        }

        private void StartScheduler()
        {
            if (m_isActive)
            {
                return;
            }
            m_isActive = true;
            Thread thread = new Thread(new ThreadStart(delegate
            {
                try
                {
                    while (m_isActive)
                    {
                        var searchEmployeeReview = m_EmployeeReviewBO.Search(new DAL.Helpers.SearchRequest<BOL.Model.EmployeeReview>()
                        {
                            Filter = f => f.Status == "Scheduled" && f.ReviewDate <= DateTime.Now
                        });
                        searchEmployeeReview.Wait();

                      
                      

                        if (searchEmployeeReview.Result.SearchTotal > 0)
                        {
                            foreach (var item in searchEmployeeReview.Result.Items)
                            {
                                item.Status = "Employee Review";

                                var updateRes = m_EmployeeReviewBO.Update(new BOL.Request<BOL.Model.EmployeeReview>(item));

                                updateRes.Wait();

                                if (!updateRes.Result.Successful)
                                    Trace.WriteLine(updateRes.Result.Message);
                            }
                            Thread.Sleep(100);
                        }

                        Thread.Sleep(100);
                    }
                }
                catch (Exception e)
                {

                }
                m_isActive = false;


            }));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
