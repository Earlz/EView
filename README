<h1>About</h1>

The EViewEngine is a view engine for .Net. It is mostly targetted at ASP.Net, but it is not tied at all to it. 

It is meant to be a very minimalistic view engine that stays out of your way and just generally makes life easier. 

It may not have all the thousands of features of the other view engines, but all you do is learn A FEW simple
things and poof, you completely understand every view generated with it, and you know the code that is generated. This engine was designed to have as few surprises as possible.

<h1>Examples</h1>

A basic example is this

    Templates/SomeView.html:
    {@ Title as string; @}
    <html><head>
    <title>{=Title=}</title>
    <body>
    Hey check out the dynamic title
    </body>
    </html>

Wherever.cs:

    var view=new SomeView();
    view.Title="Some View Title";
    response.Write(view.EViewRender()); //render returns a string

You can check out more examples in this project, which basically is just a testing harness. The real focus is in Templates/ViewGenerator.tt

<h1>Installation</h1>

To use it in your own project just simply create a Templates directory and copy Templates/ViewGenerator.tt and put it in your Templates directory. 
Modify the `Namespace` variable in the ViewGenerator.tt file to what you want(usually you want this to be the same as your project's default namespace)
Then just create a .html file in Templates and run the T4 template and poof, ViewGenerator.cs will be created and you can use your views from your other
files.