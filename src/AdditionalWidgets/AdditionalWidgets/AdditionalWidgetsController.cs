using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using AtomSite.Repository;
using AtomSite.Domain;
using System.Web.Mvc;

namespace AdditionalWidgets
{
    public class AdditionalWidgetsController : BaseController
    {
        protected IAtomPubService AtomPubService { get; private set; }
        protected IAnnotateService AnnotateService { get; private set; }
        protected IAppServiceRepository AppServiceRepository { get; private set; }
        protected IAtomEntryRepository AtomEntryRepository { get; private set; }

        public AdditionalWidgetsController(IAtomPubService atompub, IAnnotateService annotate, IAppServiceRepository svcRepo, IAtomEntryRepository entryRepo)
            : base()
        {
            AtomPubService = atompub;
            AnnotateService = annotate;
            AppServiceRepository = svcRepo;
            AtomEntryRepository = entryRepo;
        }

        [ActionOutputCache(2 * MIN)]
        public PartialViewResult RecentPostsForCategoryWidget(Include include)
        {
            var i = new AdditionalWidgetsIncludes.RecentPostsForCategoryInclude(include);

            if (!i.HasCategory)
                return new PartialViewResult();

            AtomFeed feed = AtomPubService.GetFeedByCategory(
                AppServiceRepository.GetService().GetCollection(i.Collection).Id,
                i.Category,
                null,
                0,
                i.Count > 0 ? i.Count : 5);

            return PartialView("RecentPostsForCategoryWidget", new AdditionalWidgetsModels.RecentPostsForCategoryWidgetModel
                {
                    Category = i.Category,
                    Entries = feed.Entries
                });
        }

        [ScopeAuthorize(Action = AuthAction.UpdateServiceDoc, Roles = AuthRoles.AuthorOrAdmin)]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RecentPostsForCategorySetupWidget(ConfigModel m)
        {
            var include = new AdditionalWidgetsIncludes.RecentPostsForCategoryInclude();
            if (m.IncludePath != null)
            {
                include = AppService.GetInclude<AdditionalWidgetsIncludes.RecentPostsForCategoryInclude>(m.IncludePath);
            }
            var model = GetSetupModelFromInclude(include);
            return PartialView("RecentPostsForCategorySetupWidget", model);
        }

        [ScopeAuthorize(Action = AuthAction.UpdateServiceDoc, Roles = AuthRoles.AuthorOrAdmin)]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult RecentPostsForCategorySetupWidget(AdditionalWidgetsModels.RecentPostsForCategoryWidgetSetupConfigInputModel m)
        {
            if (!ModelState.IsValidField("Category"))
                ModelState.AddModelError("Category", "Please choose a category");

            if (ModelState.IsValid)
            {
                var appSvc = AppServiceRepository.GetService();
                var include = appSvc.GetInclude<AdditionalWidgetsIncludes.RecentPostsForCategoryInclude>(m.IncludePath);
                include.Collection = m.Collection;
                include.Category = m.Category;
                include.Count = m.Count;
                AppServiceRepository.UpdateService(appSvc);
                return Json(new { success = true, includePath = m.IncludePath });
            }
            var model = GetSetupModelFromInclude(null);

            return PartialView("RecentPostsForCategorySetupWidget", model);
        }

        private IEnumerable<AppCollection> GetCollections()
        {
            var scopes = AuthorizeService.GetScopes(this.User.Identity as User);

            return scopes.Where(s => s.IsCollection)
              .Select(s => AppService.GetCollection(s.Workspace, s.Collection));
        }

        private AdditionalWidgetsModels.RecentPostsForCategoryWidgetSetupConfigModel GetSetupModelFromInclude(AdditionalWidgetsIncludes.RecentPostsForCategoryInclude include)
        {
            var model = new AdditionalWidgetsModels.RecentPostsForCategoryWidgetSetupConfigModel();
            if (include != null && include.HasCategory)
            {
                model.Category = include.Category;
                model.Collection = include.Collection;
                model.Count = include.Count;
            }

            /*
            var collectionCategories = new List<AdditionalWidgetsModels.CollectionsCategories>();
            var collections = GetCollections();
            collections.ToList().ForEach(coll =>
                {
                    var cats = GetCategories(AtomPubService.GetCollectionFeed(coll.Id, 0));
                    if (cats.Any())
                    {
                        collectionCategories.Add(new AdditionalWidgetsModels.CollectionsCategories
                        {
                            Collection = coll.Id.ToString(),
                            Categories = cats.Select(x => new SelectListItem
                                                {
                                                    Text = string.IsNullOrEmpty(x.Label) ? x.Term : x.Label,
                                                    Value = x.Term,
                                                    Selected = x.Term == model.Category && model.Collection == coll.Id.ToString()
                                                }).ToArray()
                        });
                    }
                });
            model.Collections = collections.Where(x =>
                collectionCategories.Any(p => p.Collection == x.Id.ToString()))
                .Select(o => new SelectListItem { Text = o.Title.Text, Value = o.Id.ToString() }).ToArray();
            model.CollectionCategories = collectionCategories;
            */

            var collections = GetCollections().Where(x => x.AllCategories.Any()).ToArray();

            model.Collections = collections.Select(o => new SelectListItem { Text = o.Title.Text, Value = o.Id.ToString() }).ToArray();
            model.CollectionCategories = collections.Select((c) =>
                {
                    return
                        new AdditionalWidgetsModels.CollectionsCategories
                        {
                            Collection = c.Id.ToString(),
                            Categories = c.AllCategories.Select(x => new SelectListItem
                                                {
                                                    Text = string.IsNullOrEmpty(x.Label) ? x.Term : x.Label,
                                                    Value = x.Term,
                                                    Selected = x.Term == model.Category && model.Collection == c.Id.ToString()
                                                }).ToArray()
                        };
                });


            return model;
        }

        private IEnumerable<AtomCategory> GetCategories(AtomFeed Feed)
        {
            if (Feed.Entries.Count() == 0 ||
                Feed.Entries.SelectMany(e => e.Categories).Count() == 0)
                return Enumerable.Empty<AtomCategory>();
            return Feed.Entries.SelectMany(e => e.Categories).Distinct()
                .OrderBy(c => c.ToString());
        }


    }
}
