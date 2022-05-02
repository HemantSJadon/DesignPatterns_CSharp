using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns_CSharp.behavioural
{
    public class EditorWindow
    {
        private StringBuilder text = new StringBuilder();

        /*Any user typing in the editor window > this is simulated by
        a call to this method with each keystroke, the character of the keystroke
        being passed as a method argument
        */
        public void Write(char c)
        {
            this.text.Append(c);
        }
        public string Text 
        {
            get
            {
                return this.text.ToString();
            }
        }

        /*
        user clicks copy button or hits a keystroke shortcut (ctrl + c) to copy the selected part in the 
        editor > that action gives a call to this method fetching the selected part as a string in return
        */
        public string CopySelection(Selection selectedPart)
        {
            //Span<char> destination = new Span<char>();
            char[] destination = new char[selectedPart.count];
            this.text.CopyTo(selectedPart.startIndex, destination,0, selectedPart.count);
            StringBuilder result = new StringBuilder();
            foreach(char c in destination)
                result.Append(c);
            return result.ToString();
        }
        /* user clicks cut/delete button or hits a keyboard shortcut to delete/cut 
        */
        public void DeleteSelection(Selection selectedPart)
        {
            this.text.Remove(selectedPart.startIndex, selectedPart.count);
        }
        /*
        user hits the paste button or a keyboard shortcut to paste some text from clipboard
        at the location of cursor > the location of the cursor is simulated by index
        */
        public void PasteText(int index, string value)
        {
            this.text.Insert(index, value);
        }

    }
    /*this class simulates the action of user selecting a chunk of text
        represented by a starting index and count of character selected
    */
    public class Selection
    {
        public int startIndex { get; }
        public int count { get; }
        public Selection(int startIndex, int count)
        {
            this.startIndex = startIndex;
            this.count = count;
        }
    }
    public interface ICommand_editor
    {
        public void Execute();
    }
    public class Button
    {
        private ICommand_editor command;
        public Button(ICommand_editor command)
        {
            this.command = command;
        }
        public void Click()
        {
            this.command.Execute();
        }

    }
    public class CopyCommand_editor : ICommand_editor
    {
        private TextEditingApplication app;
        public CopyCommand_editor(TextEditingApplication app)
        {
            this.app = app;
        }
        public void Execute()
        {
            this.app.ClipBoard = this.app.ActiveEditorWindow.CopySelection(this.app.CurrentUserSelection);
        }
    }
    public class CutCommand_editor : ICommand_editor
    {
        private TextEditingApplication app;
        public CutCommand_editor(TextEditingApplication app)
        {
            this.app = app;
        }
        public void Execute()
        {
            this.app.ClipBoard = this.app.ActiveEditorWindow.CopySelection(this.app.CurrentUserSelection);
            this.app.ActiveEditorWindow.DeleteSelection(this.app.CurrentUserSelection);

        }
    }
    public class PasteCommand_editor : ICommand_editor
    {
        private TextEditingApplication app;
        public PasteCommand_editor(TextEditingApplication app)
        {
            this.app = app;
        }
        public void Execute()
        {
            string textToPaste = this.app.ClipBoard;
            this.app.ActiveEditorWindow.PasteText(this.app.CurrentUserSelection.startIndex, textToPaste);
        }
    }
    public class NewCommand_editor : ICommand_editor
    {
        private TextEditingApplication app;
        public NewCommand_editor(TextEditingApplication app)
        {
            this.app = app;
        }
        public void Execute()
        {
            this.app.OpenEditorWindows.Add(new EditorWindow());
            int num_openWindows = this.app.OpenEditorWindows.Count;
            this.app.ActiveEditorWindow = this.app.OpenEditorWindows[num_openWindows-1];
            this.app.CurrentUserSelection = new Selection(0,0);
            Console.WriteLine("NewCommand: A new editor window has been opened and set as the active window.");
        }
    }
    public class CloseCommand_editor : ICommand_editor
    {
        private TextEditingApplication app;
        public CloseCommand_editor(TextEditingApplication app)
        {
            this.app = app;
        }
        public void Execute()
        {
            if(this.app.OpenEditorWindows.Count == 1)
            {
                Console.WriteLine("CloseWindowButton: Can't close the current editor window. Open one more window to close this one.");
                return;
            }
            this.app.OpenEditorWindows.Remove(this.app.ActiveEditorWindow);
            int num_openWinds = this.app.OpenEditorWindows.Count;
            this.app.ActiveEditorWindow = this.app.OpenEditorWindows[num_openWinds-1];
        }
    }
    public class HeadSection
    {
        public HeadSection(Button copy, Button cut, Button paste, Button newButton, Button close)
        {
            this.CopyButton = copy;
            this.CutButton = cut;
            this.PasteButton = paste;
            this.NewButton = newButton;
            this.CloseButton = close;
        }
        public Button CopyButton{get;}
        public Button CutButton{get;}
        public Button PasteButton {get;}
        public Button NewButton {get;}
        public Button CloseButton { get;} 
    }

    /*
    a text editing application with UI which can have multiple editor windows opened > 
    is simulated by this 
    */
    public class TextEditingApplication
    {
        /*assuming that the top/head section containing buttons remain the same 
        throughout the application, acting on the current editor window or app
        level
        */
        private EditorWindow activeEditorWindow = new EditorWindow();
        
        public TextEditingApplication()
        {
            this.OpenEditorWindows.Add(this.activeEditorWindow);
            this.Header = this.GetNewHeader();
            this.Shortcuts = SetShortcutCommands();
        }
        private HeadSection GetNewHeader()
        {
            Button copyButton = new Button(new CopyCommand_editor(this));
            Button cutButton = new Button(new CopyCommand_editor(this));
            Button pasteButton = new Button(new PasteCommand_editor(this));
            Button newButton = new Button(new NewCommand_editor(this));
            Button closeButton = new Button(new CloseCommand_editor(this));
            return new HeadSection(copyButton,cutButton, pasteButton, newButton, closeButton);
        }
        private Dictionary<string,ICommand_editor> SetShortcutCommands()
        {
            Dictionary<string, ICommand_editor> shortcuts = new Dictionary<string, ICommand_editor>();
            var copy = new CopyCommand_editor(this);
            var cut = new CutCommand_editor(this);
            var paste = new PasteCommand_editor(this);
            var newCommand = new NewCommand_editor(this);
            var close = new CloseCommand_editor(this);
            shortcuts.Add("ctrl+c",copy);
            shortcuts.Add("ctrl+x",cut);
            shortcuts.Add("ctrl+v",paste);
            shortcuts.Add("ctrl+n",newCommand);
            shortcuts.Add("ctrl+q",close);
            return shortcuts;
        }
        public EditorWindow ActiveEditorWindow {
            get
            {
                return this.activeEditorWindow;
            }
            set
            {
                this.activeEditorWindow = value;
            }
        }
        public List<EditorWindow> OpenEditorWindows { get;internal set;} = new List<EditorWindow>();
        public HeadSection Header {get; private set;}
        public string ClipBoard {get;set;} = String.Empty;
        public Selection CurrentUserSelection { get; set; } = new Selection(0,0);
        public Dictionary<string, ICommand_editor> Shortcuts {get;private set;}
        public void HitAShortcut(string keyCombo)
        {
            if(this.Shortcuts.ContainsKey(keyCombo))
                this.Shortcuts[keyCombo].Execute();
            else
                Console.WriteLine($"The entered key combo {keyCombo} does not have a command registered against it.");

        }

    }
    public class Client_command_editor
    {
        public void ClientCode()
        {
            //user opening new application instance
            Console.WriteLine("Client: user has opened a new instance of editing application... ");
            TextEditingApplication app = new TextEditingApplication();
            Console.WriteLine("Client: user clicks the new window button...");
            app.Header.NewButton.Click();
            int startIndex = -1;
            int charCount = 0;
            Console.WriteLine("Client: user is tying the following sentence into the active editor window... ");
            string textToWrite = "Welcome to the grand opening event!";
            Console.WriteLine(textToWrite);
            charCount += textToWrite.Length;
            foreach(char c in textToWrite)
                app.ActiveEditorWindow.Write(c);
            Console.WriteLine("Client: the current active editor window has the following typed in it ...");
            Console.WriteLine(app.ActiveEditorWindow.Text);
            Console.WriteLine("Client: user selects a part starting from character 'g' of the word 'grand' to 'g' of the word 'opening' ...");
            app.CurrentUserSelection = new Selection(15,13); //starting at index 15, count of characters in selection= 13
            Console.WriteLine("Client: user clicks the copy button to copy the selected part ... ");
            app.Header.CopyButton.Click();
            Console.WriteLine("Client: user clicks the new window button...");
            app.Header.NewButton.Click();
            string keyComboToPaste = "ctrl+v";
            Console.WriteLine($"Client: user hits the keycombo {keyComboToPaste} to paste the copied text into the newly opened window...");
            //app.Header.PasteButton.Click();
            app.HitAShortcut(keyComboToPaste);
            Console.WriteLine("Client: the current active editor window has the following typed in it ...");
            Console.WriteLine(app.ActiveEditorWindow.Text);
            




            
        }
    }

}