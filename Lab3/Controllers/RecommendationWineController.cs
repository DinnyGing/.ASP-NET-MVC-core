using Lab3.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class RecommendationWineController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FillIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillIn(WineParam param)
        {
            try
            {
                List<RecommendationView> recommendations = new List<RecommendationView>();
                for (float i = param.MinDensity; i < param.MaxDensity; i += 0.001f)
                {
                    for (float j = param.MinAlcohol; j < param.MaxAlcohol; j+= 0.1f)
                    {
                        var sampleData = new MLModel.ModelInput()
                        {
                            Density = i,
                            Alcohol = j,
                            Quality = param.Quality

                        };
                        var res = MLModel.Predict(sampleData);
                        if(res.Score > 0)
                        {
                            RecommendationView recommendation = new RecommendationView()
                            {
                                Density = res.Density,
                                Alcohol = res.Alcohol,
                                Quality = res.Quality,
                                Score = res.Score
                            };
                            recommendations.Add(recommendation);
                        }
                    }
                }
                
                return View("Details", recommendations);
            }
            catch
            {
                return View();
            }
        }
    }
}
