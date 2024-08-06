using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Utilities;

namespace IlluminatedComments
{
    [Export(typeof(ILineTransformSourceProvider))]
    [ContentType(ContentTypes.Cpp)]
    [ContentType(ContentTypes.CSharp)]
    [ContentType(ContentTypes.VisualBasic)]
    [ContentType(ContentTypes.FSharp)]
    [ContentType(ContentTypes.JavaScript)]
    [ContentType(ContentTypes.TypeScript)]
    [ContentType(ContentTypes.Python)]
    [ContentType(ContentTypes.Java)]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal class CommentLineTransformSourceProvider : ILineTransformSourceProvider
    {
        [Import] internal SVsServiceProvider ServiceProvider;

        [Import] public ITextDocumentFactoryService TextDocumentFactory { get; set; }

        ILineTransformSource ILineTransformSourceProvider.Create(IWpfTextView view)
        {
            var manager = view.Properties.GetOrCreateSingletonProperty(() => new CommentsAdornment(view, TextDocumentFactory, ServiceProvider));
            return new CommentLineTransformSource(manager);
        }
    }
}