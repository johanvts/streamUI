namespace visualizer

open System.Windows.Forms
open System.Drawing

type Stream<'a> =
    abstract member next: unit -> 'a

type SAlgo<'a> =
    abstract member put: 'a -> unit
    abstract member result: 'a
    abstract member count: int

type Drawer(name,stream:Stream<int>,Algo:SAlgo<int>)=
     let form = new Form(Text=name)
     
     let newMenu (s:string)=new ToolStripMenuItem(s,Checked= true,CheckOnClick = true)
     let menuVertices = newMenu "ShowVertices"
     let menuEdges = newMenu "Show &Edges"
     let menuFill = newMenu "Fill Polygon"

     let nextBtn = new System.Windows.Forms.Button(Text = "Next",Top=40)
     let lastLbl = new Label(Top=40, Left = 80,Text = "No items reviceved yet")
     let countLbl = new Label(Top=40, Left = 200, Text = "0")
     let resultLbl = new Label(Top=80, Left = 80,Text = Algo.ToString())

     let updatecycle next =
        lastLbl.Text <- string next
        Algo.put next
        resultLbl.Text <-string (Algo.result)
        countLbl.Text <- string (Algo.count)

     let setupMenu()=
       let menu = new MenuStrip()
       let fileMenuItem = new ToolStripMenuItem("&File")
       let settMenuItem = new ToolStripMenuItem("&Settings")
       let exitMenuItem = new ToolStripMenuItem("&Exit")
       menu.Items.Add(fileMenuItem)|>ignore
       menu.Items.Add(settMenuItem)|>ignore
       fileMenuItem.DropDownItems.Add(exitMenuItem)|>ignore
       settMenuItem.DropDownItems.Add(menuVertices)|>ignore
       settMenuItem.DropDownItems.Add(menuEdges)|>ignore
       settMenuItem.DropDownItems.Add(menuFill)|>ignore
       exitMenuItem.Click.Add(fun _-> form.Close())
       menuVertices.Click.Add(fun _-> form.Invalidate())
       menuEdges.Click.Add(fun _-> form.Invalidate())
       menuFill.Click.Add(fun _-> form.Invalidate())
       nextBtn.Click.Add(fun _-> updatecycle (stream.next()))
       menu
       
     member d.run =
      form.Controls.Add(nextBtn)
      form.Controls.Add(lastLbl)
      form.Controls.Add(resultLbl)
      form.Controls.Add(countLbl)
      form.MainMenuStrip <- setupMenu()
      form.Controls.Add(form.MainMenuStrip)
      Application.Run(form)
     


