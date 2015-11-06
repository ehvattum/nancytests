using System;
using System.Threading.Tasks;

using Nancy;

namespace NancyApplication1
{
    public class IndexModule : NancyModule
    {
        private readonly ITask task;


        public IndexModule(ITask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            this.task = task;
            Get["/"] = parameters => GetValue();
            Get["/async", true] = (_, c) => GetValueAsync();
            Get["/asyncobj", true] = (_, c) => GetValueObjectAsync();
        }


        private async Task<dynamic> GetValueObjectAsync()
        {
            return Response.AsJson(await this.task.GetObjectAsync());
        }


        private async Task<dynamic> GetValueAsync()
        {
            var value = await this.task.GetValueAsync();
            if (value == null)
                throw new ApplicationException("i was null!");

            return value;
        }


        private dynamic GetValue()
        {
            var value = this.task.GetValue();
            if (value == null)
                throw new ApplicationException("i was null!");

            return value;
        }
    }

    public interface ITask
    {
        string GetValue();
        Task<string> GetValueAsync();
        Task<ValueObject> GetObjectAsync();
    }
}