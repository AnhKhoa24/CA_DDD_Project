
using Application.Menus;
using UnitTests.Application.UnitTests.TestUtils.Constants;

///Builder Pattern
namespace UnitTests.Application.UnitTests.Menus.Commands.TestUtils;

public class CreateMenuCommandBuilder
{
    private string _name = Constants.Menu.Name;
    private string _description = Constants.Menu.Description;
    private string _hostId = Constants.Menu.HostId;
    private List<MenuSectionCommand> _sections = new()
    {
        new MenuSectionCommandBuilder().Build()
    };

    public CreateMenuCommandBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CreateMenuCommandBuilder WithEmptyName()
    {
        _name = string.Empty;
        return this;
    }

    public CreateMenuCommandBuilder WithEmptyDescription()
    {
        _description = string.Empty;
        return this;
    }

    public CreateMenuCommandBuilder WithNoSections()
    {
        _sections = new List<MenuSectionCommand>();
        return this;
    }

    public CreateMenuCommandBuilder WithSections(params MenuSectionCommand[] sections)
    {
        _sections = sections.ToList();
        return this;
    }

    public CreateMenuCommand Build()
    {
        return new CreateMenuCommand(_name, _description, _hostId, _sections);
    }
}
public class MenuSectionCommandBuilder
{
    private string _name = Constants.Menu.SectionNameFromIndex();
    private string _description = Constants.Menu.SectionDescriptionFromIndex();
    private List<MenuItemCommand> _items = new()
    {
        new MenuItemCommandBuilder().Build()
    };

    public MenuSectionCommandBuilder WithEmptyName()
    {
        _name = string.Empty;
        return this;
    }

    public MenuSectionCommandBuilder WithEmptyDescription()
    {
        _description = string.Empty;
        return this;
    }

    public MenuSectionCommandBuilder WithNoItems()
    {
        _items = new List<MenuItemCommand>();
        return this;
    }

    public MenuSectionCommandBuilder WithItems(params MenuItemCommand[] items)
    {
        _items = items.ToList();
        return this;
    }

    public MenuSectionCommand Build()
    {
        return new MenuSectionCommand(_name, _description, _items);
    }
}
public class MenuItemCommandBuilder
{
    private string _name = Constants.Menu.ItemName;
    private string _description = Constants.Menu.ItemDescription;

    public MenuItemCommandBuilder WithEmptyName()
    {
        _name = string.Empty;
        return this;
    }

    public MenuItemCommandBuilder WithEmptyDescription()
    {
        _description = string.Empty;
        return this;
    }

    public MenuItemCommand Build()
    {
        return new MenuItemCommand(_name, _description);
    }
}
