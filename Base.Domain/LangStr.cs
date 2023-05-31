namespace Base.Domain;

public class LangStr : Dictionary<string, string>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    private const string DefaultCulture = "en";

    
    public LangStr() : this("", Thread.CurrentThread.CurrentUICulture.Name)
    {
        
    }
    
    public LangStr(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
    {
        
    }
    
    public LangStr(string value, string culture)
    {
        this[culture] = value;
    }

    public string? Translate(string? culture = null)
    {
        if (this.Count == 0) return null;

        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;

        if (ContainsKey(culture))
        {
            return this[culture];
        }
        

        var neutralCulture = culture.Split("-")[0];
        if (ContainsKey(neutralCulture))
        {
            return this[neutralCulture];
        }
        if (ContainsKey(DefaultCulture))
        {
            return this[DefaultCulture];
        }

        return null;


        // object - query
        // en-GB - en-GB - done
        // en - en-GB
        // en-GB - en-US - skip
        // en-GB - ru
        // null - ru

    }
    
    public void SetTranslation(string value)
    {
        this[Thread.CurrentThread.CurrentUICulture.Name] = value;
    }
    
    public override string ToString()
    {
        return Translate() ?? "???";
    }

    // string xxx = new LangStr("zzz")
    public static implicit operator string(LangStr? langStr) => langStr?.ToString() ?? "null";
    
    // LangStr lStr = "xxx"; // internally it will be lStr = new LangStr("xxx");
    public static implicit operator LangStr(string value) => new (value);

}