﻿using System;
using System.Data.Entity;
using NUnit.Framework;
//using nTestSwarmTests.App_Start;

[SetUpFixture]
public class TestAssemblySetup
{
    [SetUp]
    public void Setup()
    {
        //EntityFramework_SqlServerCompact.Start();
        if (Database.Exists("nTestSwarmContext.sdf"))
        {
            try
            {
                Database.Delete("nTestSwarmContext.sdf");
            }
            catch (Exception)
            {
                
            }
        }
    }
}