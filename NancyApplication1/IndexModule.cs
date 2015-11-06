using Nancy;

namespace NancyApplication1
{
    public class IndexModule : NancyModule
    {
        private readonly ITask task;


        public IndexModule(ITask task)
        {
            this.task = task;
            Get["/"] = parameters => task.GetValue();
        }
    }

    public interface ITask
    {
        string GetValue();
    }
}