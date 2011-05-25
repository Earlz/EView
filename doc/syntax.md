Here I will attempt to give a brief overview of all of the different "blocks" as I call them. 

h1. Output Block

For starters, there is the Output Block

<pre>{= Title =}</pre>

This block will take whatever variable you give it and write it to the output stream. It generates something like this:

<pre>__Output.Write(Title.ToString());</pre>

h2. Foreach handling in Output Blocks

An output block, when printing a variable, will first try to cast the variable as an IEnumerable. If this cast succeeds, then the variable is treated as an IEnumerable and it will foreach through the variable and print each list item as normal. 

h3. Not quite right things

You can also do some interesting things because of the way it is implemented. For instance you can do:

<pre>{= MyStaticClass.MyValue =} </pre>

Basically, as long as it returns a value and can be ended by a semi-colon, it can be used. Note though that whatever code you put in such a block may be executed more than once. 

h1. Variable Block

The variable block is currently pretty buggy, but I will describe here how I want it to actually be. Currently, the main limitation is spaces aren't allowed in type names(so List< MyList > is invalid)

Variable blocks are used when you want to declare variables ahead of time and/or as a specific type other than string.

The general syntax is:
<pre>
{@
  MyVariable as MyType;
@}
</pre>

MyType can be any number of things including lists and other complex types. It generates code similar to this:

<pre>
public MyType MyVariable{get;set;}
</pre>

h1. Internal Code Block

The internal code block is useful for when you need to do a bit of custom work while rendering. It is good practice to only use these when necessary and keep the heavy lifting code outside of views.

The general syntax (with a C# if statement) is this:
<pre>
{#
  if(Title=="Foo"){
#}
Foo was the title
{#
  }else{
#}
Foo was not the title
{#
  }
#}
</pre>

One thing to note here is that I don't use the single-statement style of of the if statement. If I had not included the curly brackets for the if statement then the behavior would be undefined. It would probably work as expected right now, but I don't guarantee that it will in the future. 

This basically will generate code similar to this:
<pre>
void BuildOutput(){
  if(Title=="Foo"){
    __output.Append("Foo was the title");
  }else{
    __output.Append("Foo was not the title");
  }
}
</pre>

If you need to write out some text from within an Internal Code Block, then use the Write() function:

<pre>
{#
  Write("I needed to write some urgent things!");
#}
</pre>

h1. External Code Block

The external code block is used when you need to create helper functions and other things that need to be done outside of the BuildOutput function. An example is this:
<pre>
{#
  PrintFoo();
#}

{+
  void PrintFoo(){
    Write("Print Foo");
  }
+}
</pre>

This will generate code like this:
<pre>
void BuildOutput(){
  PrintFoo();
}

void PrintFoo(){
  Write("Print Foo");
}
</pre>

Again, the same notice applies as with internal code blocks. It is generally regarded as bad practice to put a lot of heavy code into your views. It is recommended to put only code needed for rendering in views and then have domain specific code outside of the views.

h1. Raw Output Block

The Raw Output Block basically just writes whatever code you provide into the `Write` function. For example:

<pre>
{-Foo.Bar()-}
</pre>

Will generate the equivalent of

<pre>
Write(Foo.Bar());
</pre>

The main difference between this and the output block is that the output block will try to find if the object is an IEnumerable or an EView and render it different. With the raw output block, this does not happen and what you write is put straight into a Write function

h1. Keyword Blocks

Keyword blocks are special blocks that are done a bit differently. One example is the Base Keyword Block:
<pre>
{!base=MyBaseClass!}
</pre>
This will make it so that the current view derives from the MyBaseClass instead of the default base class. Keyword blocks are usually more for configuration purposes, though there are a few rendering functions in them planned.

The Name Keyword Block:
<pre>
{!name=MyClassName!}
</pre>

With this block you can override what the view's class name will be when generated. 



h1. Planned future blocks

I also am planning on implementing a few more blocks. One of them is the if block

<pre>
{!if Title=="Foo" !}
  Title is Foo!
{!else!}
  Title is not Foo!
{!endif!}
</pre>

