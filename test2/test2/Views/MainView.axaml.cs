using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace test2.Views;

public partial class MainView : UserControl
{
    private readonly TextBlock _dropState;
    private const string CustomFormat = "application/xxx-avalonia-controlcatalog-custom";
    
    public MainView()
    {
        AvaloniaXamlLoader.Load(this);
        _dropState = this.Get<TextBlock>("DropState");

        int textCount = 0;
        SetupDnd("Text", d => d.Set(DataFormats.Text,
            $"Text was dragged {++textCount} times"), DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);

        SetupDnd("Custom", d => d.Set(CustomFormat, "Test123"), DragDropEffects.Move);
    }
    private void SetupDnd(string suffix, Action<DataObject> factory, DragDropEffects effects)
        {
            var dragMe = this.Get<Border>("DragMe" + suffix);
            var dragState = this.Get<TextBlock>("DragState" + suffix);

            void DoDrag(object? sender, Avalonia.Input.PointerPressedEventArgs e)
            {
                var dragData = new DataObject();
                factory(dragData);
            }

            void DragOver(object? sender, DragEventArgs e)
            {
                if (e.Source is Control c && c.Name == "MoveTarget")
                {
                    e.DragEffects = e.DragEffects & (DragDropEffects.Move);
                }
                else
                {
                    e.DragEffects = e.DragEffects & (DragDropEffects.Copy);
                }

                // Only allow if the dragged data contains text or filenames.
                if (!e.Data.Contains(DataFormats.Text)
                    && !e.Data.Contains(CustomFormat))
                    e.DragEffects = DragDropEffects.None;
            }

            async void Drop(object? sender, DragEventArgs e)
            {
                if (e.Source is Control c && c.Name == "MoveTarget")
                {
                    e.DragEffects = e.DragEffects & (DragDropEffects.Move);
                }
                else
                {
                    e.DragEffects = e.DragEffects & (DragDropEffects.Copy);
                }

                if (e.Data.Contains(DataFormats.Text))
                {
                    _dropState.Text = e.Data.GetText();
                }
                else if (e.Data.Contains(CustomFormat))
                {
                    _dropState.Text = "Custom: " + e.Data.Get(CustomFormat);
                }
            }

            dragMe.PointerPressed += DoDrag;

            AddHandler(DragDrop.DropEvent, Drop);
            AddHandler(DragDrop.DragOverEvent, DragOver);
        }

    // private void Button_OnClick(object? sender, RoutedEventArgs e)
    // {
    //     QuestPDF.Settings.License = LicenseType.Community;
    //     var document = Document.Create(container =>
    //     {
    //         container.Page(page =>
    //         {
    //             page.Size(PageSizes.A4);
    //             page.Header()
    //                 .Text("Hello world!").SemiBold().FontSize(30).FontColor(Colors.Amber.Medium);
    //             page.Content()
    //                 .PaddingVertical(1, Unit.Centimetre)
    //                 .Column(x =>
    //                 {
    //                     x.Spacing(20);
    //                     x.Item().Text(Placeholders.LoremIpsum());
    //                     x.Item().Image(Placeholders.Image);
    //                     x.Item().Text(Placeholders.LoremIpsum());
    //
    //                     // x.Item().Image(bytes);
    //                 });
    //         });
    //     });
    //     if (File.Exists("estearchivonoexiste1.pdf"))
    //     {
    //         var siExiste = true;
    //     }
    //     document.GeneratePdf("estearchivonoexiste1.pdf");
    //     if (File.Exists("estearchivonoexiste1.pdf"))
    //     {
    //         var siExiste = true;
    //     }
    // }
}