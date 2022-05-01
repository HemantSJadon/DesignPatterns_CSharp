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
    public abstract class Button
    {
        protected TextEditingApplication app;
        public Button(TextEditingApplication app)
        {
            this.app = app;
        }
        public abstract void Click(Selection selectedPart);
    }
    public class CopyButton : Button
    {
        public CopyButton(TextEditingApplication app) : base(app)
        {
            
        }
        public override void Click(Selection selectedPart)
        {
            this.app.ClipBoard = this.app.ActiveEditorWindow.CopySelection(selectedPart);
        }
    }
    public class CutButton : Button
    {
        public CutButton(TextEditingApplication app) : base(app)
        {}
        public override void Click(Selection selectedPart)
        {
            this.app.ClipBoard = this.app.ActiveEditorWindow.CopySelection(selectedPart);
            this.app.ActiveEditorWindow.DeleteSelection(selectedPart);

        }
    }
    public class PasteButton : Button
    {
        public PasteButton(TextEditingApplication app) : base(app)
        {}
        public override void Click(Selection selectedPart)
        {
            string textToPaste = this.app.ClipBoard;
            this.app.ActiveEditorWindow.PasteText(selectedPart.startIndex, textToPaste);
        }
    }
    public class NewButton : Button
    {
        public NewButton(TextEditingApplication app) : base(app)
        {}
        public override void Click(Selection selectedPart = null)
        {
            this.app.OpenEditorWindows.Add(new EditorWindow());
            int num_openWindows = this.app.OpenEditorWindows.Count;
            this.app.ActiveEditorWindow = this.app.OpenEditorWindows[num_openWindows-1];
            Console.WriteLine("NewButton: A new editor window has been opened and set as the active window.");
        }
    }
    public class CloseWindowButton : Button
    {
        public CloseWindowButton(TextEditingApplication app) : base(app)
        {}
        public override void Click(Selection selectedPart)
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
        public HeadSection(CopyButton copy, CutButton cut, PasteButton paste, NewButton newButton, CloseWindowButton close)
        {
            this.CopyButton = copy;
            this.CutButton = cut;
            this.PasteButton = paste;
            this.NewButton = newButton;
            this.CloseButton = close;
        }
        public CopyButton CopyButton{get;}
        public CutButton CutButton{get;}
        public PasteButton PasteButton {get;}
        public NewButton NewButton {get;}
        public CloseWindowButton CloseButton { get;} 
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
        // private List<EditorWindow> openWindows = new List<EditorWindow>();
        
        public TextEditingApplication()
        {
            this.OpenEditorWindows.Add(this.activeEditorWindow);
            this.Header = new HeadSection(new CopyButton(this), new CutButton(this), new PasteButton(this), new NewButton(this), new CloseWindowButton(this));
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
            Selection userSelection = new Selection(15,13); //starting at index 15, count of characters in selection= 13
            Console.WriteLine("Client: user clicks the copy button to copy the selected part ... ");
            app.Header.CopyButton.Click(userSelection);
            Console.WriteLine("Client: user clicks the new window button...");
            app.Header.NewButton.Click();
            Console.WriteLine("Client: user clicks the paste button to paste the copied text into the newly opened window...");
            app.Header.PasteButton.Click(new Selection(0,0));
            Console.WriteLine("Client: the current active editor window has the following typed in it ...");
            Console.WriteLine(app.ActiveEditorWindow.Text);
            




            
        }
    }

}