// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructuremapMvc.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using StructureMap;
using nTestSwarm.DependencyResolution;
using System.Diagnostics;

[assembly: WebActivator.PreApplicationStartMethod(typeof(nTestSwarm.App_Start.StructuremapMvc), "Start")]

namespace nTestSwarm.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
			IContainer container = IoC.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
            GlobalHost.DependencyResolver=new SignalRDependencyResolver(container);

#if DEBUG
            var registrations = container.WhatDoIHave();

            Debug.Write(registrations);
#endif
        }
    }
}