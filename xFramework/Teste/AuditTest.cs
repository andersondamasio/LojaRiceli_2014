using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Modelo;
using EntityFramework.Audit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Teste
{
    [TestClass]
    public class AuditTest
    {

      /*  [TestMethod]
        public void CreateLog2()
        {
            AuditConfiguration.Default.IncludeRelationships = true;
            AuditConfiguration.Default.LoadRelationships = true;

            AuditConfiguration.Default.IsAuditable<Task>();
            AuditConfiguration.Default.IsAuditable<User>();

            var db = new  Context();
            var audit = db.BeginAudit();

            var task = db.Tasks.Find(1);
            Assert.IsNotNull(task);

            task.PriorityId = 2;
            task.StatusId = 2;
            task.Summary = "Summary: " + DateTime.Now.Ticks;

            var log = audit.CreateLog();
            Assert.IsNotNull(log);

            string xml = log.ToXml();
            Assert.IsNotNull(xml);
        }


        public static object FormatStatus(AuditPropertyContext auditProperty)
        {
            Console.WriteLine("FormatStatus: {0}", auditProperty.Value);
            return "Status: " + auditProperty.Value;
        }*/




    }
}
