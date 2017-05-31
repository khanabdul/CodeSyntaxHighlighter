# CodeSyntaxHighlighter
Create code Hightlighter for blogs /web 
## Getting Started
This Project can be used to show code in the blogs and website as they look in the IDE i.e properly indeted and coloured.
Run the project to generate the HTML string which can be directly put into BLOG/WEBSITE.

# Example C# code : 

```
while (dataReader.Read())
        {  
            obj = new T();

            foreach (PropertyInfo prop in PropertyInfo)
            {
                if (DataConverterHelper.ColumnExists(dataReader,prop.Name) && !dataReader.IsDBNull(prop.Name))
                {
                    prop.SetValue(obj, dataReader[prop.Name], null);
                }
            }
            yield return obj;
 ```           
  # Beautified  Generated HTML : to be put into BLOG or WEBSITE
```  
<div style="color:Black;background-color:White;">
<pre>
<span style="color:Blue;">while</span> (dataReader.Read())
        {  
            obj = <span style="color:Blue;">new</span> T();

            foreach (PropertyInfo prop <span style="color:Blue;">in</span> PropertyInfo)
            {
                <span style="color:Blue;">if</span> (DataConverterHelper.ColumnExists(dataReader,prop.Name) &amp;&amp; !dataReader.IsDBNull(prop.Name))
                {
                    prop.SetValue(obj, dataReader[prop.Name], <span style="color:Blue;">null</span>);
                }
            }
            yield <span style="color:Blue;">return</span> obj;
</pre>
</div>
```
