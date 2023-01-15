using GGJWeb.Data;
using GGJWeb.Models;

namespace GGJWeb.Utils
{
    public class DBUtils
    {

        public static async Task InitDB(DataContext context)
        {

            PostInfo info = new() { Body = @"<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam gravida posuere lorem non tristique. Ut ut urna mauris. Fusce sit amet vulputate eros. Praesent semper leo vel imperdiet mattis. Nulla eget velit suscipit, bibendum mauris eget, dapibus augue. Vestibulum pharetra nunc nec libero sodales, id egestas libero porta. Donec vitae finibus orci.</p>
<p>Cras dapibus scelerisque aliquet. Morbi consectetur hendrerit elit, id consectetur lorem sodales iaculis. Maecenas ut accumsan mauris, at euismod risus. Sed interdum fermentum diam tempor vestibulum. Aliquam erat volutpat. Ut sit amet nisi turpis. Donec sagittis sem diam, ac mollis nisi dictum id. Sed pulvinar dignissim enim. Pellentesque suscipit nisl nec nunc tempor scelerisque. Nunc bibendum diam augue, mollis lacinia neque ullamcorper vitae. Proin sit amet metus consectetur, suscipit diam at, euismod metus. Curabitur ultricies mi nec cursus mattis.</p>
<p>Nunc in pretium lorem, a suscipit lorem. Duis et sem volutpat, congue arcu at, eleifend magna. Suspendisse id ante eget sapien maximus tempus. Integer cursus risus cursus nisl luctus, a euismod enim egestas. Etiam blandit libero sit amet egestas scelerisque. Donec turpis sapien, tempor non sapien aliquam, vehicula finibus quam. Praesent ligula lorem, tincidunt et euismod sed, ultricies sed tellus. Suspendisse potenti. Donec fermentum vel orci vel interdum. Morbi accumsan cursus nisi id sodales. Duis in diam sem. Ut vitae mollis libero.</p>
<p>Vivamus eu erat non leo varius tempor. In vitae ipsum convallis turpis commodo laoreet. Ut mollis turpis ante. Nunc ut quam ullamcorper, placerat leo eget, ultricies dolor. Nam laoreet imperdiet eros eget vulputate. Aliquam efficitur ipsum et enim sollicitudin tempor. Suspendisse sollicitudin enim vel ante sodales, id iaculis augue porttitor. Duis ut venenatis ipsum, sed tristique mauris. Mauris congue, orci in vehicula elementum, arcu quam ullamcorper neque, quis vulputate purus purus ac dolor. Donec convallis, tortor a rhoncus egestas, massa mauris venenatis est, vel auctor quam turpis ornare enim.</p>
<p>Mauris ac imperdiet libero. Ut suscipit felis sit amet sem elementum, sed pharetra ex cursus. Vivamus efficitur ac nibh eu molestie. Morbi sollicitudin dui sit amet ex laoreet interdum. Duis tristique urna in dui porttitor mattis. Fusce faucibus leo vel faucibus fermentum. Quisque in enim urna. Aenean tincidunt sapien nisl, in interdum magna viverra eu. Etiam et vehicula dolor.</p>"
            };
            Post p = new()
            {
                Title = "Título del Post",
                Subtitle = "Subtítulo del Post",
                PostInfo = info,
                PublishedOn = DateTime.UtcNow,
            };

            await context.AddAsync(p);
            await context.SaveChangesAsync();
        }

    }
}
