/*
Copyright (c) 2010 - 2011 Jordan "Earlz/hckr83" Earls  <http://www.Earlz.biz.tm>
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
3. The name of the author may not be used to endorse or promote products
   derived from this software without specific prior written permission.
   
THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL
THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS;
OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

This file is part of the EViewEngine project.
*/

using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using Earlz.EView;
using System.Diagnostics;
using System.Text;

namespace EViewEngine
{
	public class TestHandler : System.Web.IHttpHandler
	{

		public virtual bool IsReusable {
			get { return false; }
		}
		StringBuilder TempOutput=new StringBuilder();
		
		public virtual void ProcessRequest (HttpContext context)
		{
			if(context.Request.QueryString["id"]=="1"){
				var v=new TestViewClass();
				context.Response.ContentType="text/html";
				v.Title="Test Handler Page";
				v.SomeNumber=7;
				var l=new List<TestListItem>();
				l.Add(new TestListItem());
				l.Add(new TestListItem());
				l.Add(new TestListItem());
				l.Add(new TestListItem());
				v.ContentList=l;
				context.Response.Write(v.EViewRender());
			}else if(context.Request.QueryString["id"]=="2"){
				var v=new TestChild();
				context.Response.Write(v.EViewRender());
			}else if(context.Request.QueryString["id"]=="3"){ //performance test
				Stopwatch sw = new Stopwatch();
				sw.Start();
				const int ITERATIONS=1000;
				for(int i=0;i<ITERATIONS;i++){
					PerfTest();
				}
				sw.Stop();
				context.Response.Write("Did "+ITERATIONS+" iterations in "+sw.ElapsedMilliseconds+"ms <br />");
				context.Response.Flush();
				context.Response.Write("Output: "+TempOutput.ToString());
			}
		}
		
		void PerfTest ()
		{
			var view=new PerformanceView();
			view.test1="Test 1 string";
			view.test2=10;
			view.test3=null;
			view.test4=new PerformanceView{
				test1="recursive test 1 string",
				test2=20,
				test3=15,
				test4=null,
			};
			TempOutput.Append(view.EViewRender());
		}

		
	}
	abstract class TestViewBase : IEView{
		public abstract string EViewRender();
	}
}

