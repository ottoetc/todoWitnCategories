using Nancy;
using System.Collections.Generic;
using ToDo.Objects;

namespace ToDoList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/categories"] = _ => {
        var allCategories = Category.GetAll();
        return View["categories.cshtml", allCategories];
      };
      Get["/categories/new"] = _ => {
        return View["category_form.cshtml"];
      };
      Post["/categories"] = _ => {
        var newCategory = new Category(Request.Form["category-name"]);
        var allCategories = Category.GetAll();
        return View["categories.cshtml", allCategories];
      };
      Get["/categories/{id}"] = parameters => {
        var model = new Dictionary <string, object>();
        var selectedCategory = Category.Find(parameters.id);
        var categoryTasks = selectedCategory.GetTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", categoryTasks);
        return View["category.cshtml", model];
      };
      Get["/categories/{id}/tasks/new"] = parameters => {
        var model = new Dictionary<string, object>();
        var selectedCategory = Category.Find(parameters.id);
        var allTasks = selectedCategory.GetTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", allTasks);
        return View["category_tasks_form.cshtml", model];
      };
      Post["/tasks"] = _ => {
        var model = new Dictionary<string, object>();
        var selectedCategory = Category.Find(Request.Form["category-id"]);
        var categoryTasks = selectedCategory.GetTasks();
        string taskDescription = Request.Form["task-description"];
        var newTask = new Task(taskDescription);
        categoryTasks.Add(newTask);
        model.Add("tasks", categoryTasks);
        model.Add("category", selectedCategory);
        return View["category.cshtml", model];
      };
    }
  }
}
