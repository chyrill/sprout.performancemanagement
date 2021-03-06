﻿using System;
using System.Net.Http;
using System.Web.Http.Tracing;

namespace al.performancemanagement.App
{
    public class WebApiTracer: System.Web.Http.Tracing.ITraceWriter
    {
       
        public void Trace(HttpRequestMessage request, string category, System.Web.Http.Tracing.TraceLevel level, Action<TraceRecord> traceAction)
        {
            TraceRecord rec = new TraceRecord(request, category, level);
            traceAction(rec);
            WriteTrace(rec);
        }

        protected void WriteTrace(TraceRecord rec)
        {
           
                var message = string.Format("{0}:{1}:{2}", rec.Operator, rec.Operation, rec.Message);
               
            
        }
    }
}