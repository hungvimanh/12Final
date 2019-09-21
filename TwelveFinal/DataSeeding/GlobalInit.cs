using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class GlobalInit
    {
        public TFContext tFContext;

        public AreaInit AreaInit;
        public PriorityTypeInit PriorityTypeInit;
        public GlobalInit(TFContext tFContext)
        {
            this.tFContext = tFContext;
            AreaInit = new AreaInit(tFContext);
            PriorityTypeInit = new PriorityTypeInit(tFContext);
        }

        public bool Init()
        {
            Clean();
            AreaInit.Init();
            PriorityTypeInit.Init();
            return true;
        }

        public void Clean()
        {
            string condition = "IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationLog]''),0),ISNULL(OBJECT_ID(''[dbo].[__SchemaSnapshot]''),0))";
            string command = string.Format(
              @"EXEC sp_MSForEachTable '{0} ALTER TABLE ? NOCHECK CONSTRAINT ALL';
                EXEC sp_MSForEachTable '{0} BEGIN SET QUOTED_IDENTIFIER ON; DELETE FROM ? END';
                EXEC sp_MSForEachTable '{0} ALTER TABLE ? CHECK CONSTRAINT ALL';", condition);
            var result = tFContext.Database.ExecuteSqlCommand(command);
        }
    }
}
