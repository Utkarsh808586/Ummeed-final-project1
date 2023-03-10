using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UMMEED.Models;
using BOL;
using BLL;
namespace UMMEED.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        return View();
    }
    [HttpGet]
    public IActionResult Bemyfriend(){
        return View();
    }
    
    [HttpGet]
    public IActionResult Registration(){
        return View();
    }


    [HttpGet]
        public IActionResult ShowContributors(){
       IOManage im = new IOManage();
       List<Contributors> clist  =im.GetContributors();
       if(clist!=null){
        ViewData["Cont"]=clist;
       }

       return View();
    }

    [HttpGet]
    public IActionResult Anonymus(){
         IOManage im = new IOManage();
         List<Account>alist = im.GetAccount();
        foreach(Account ac in alist){
            if(alist!=null){
            ViewData["Account"]=ac;
          }
        }
        return View();
    }
    [HttpPost]
    public IActionResult Anonymus(int accid,int payment){
        IOManage im = new IOManage();
         
         List<Account>alist = im.GetAccount();
        foreach(Account ac in alist){
            if(alist!=null){
               int UpdatedAmount = ac.Amount+payment;
               bool status = im.NewAvailbleAmount(accid,UpdatedAmount);
               if(status){
                 TempData["message"]="Thanks For Your SuccessFull Donation";
                 return RedirectToAction("Donation","Home");
               }   
            }
        }
        
        return View();
    }


    [HttpGet]
    public IActionResult NonAnonymus(){
         IOManage im = new IOManage();
         List<Account>alist = im.GetAccount();
        foreach(Account ac in alist){
            if(alist!=null){
            ViewData["Account"]=ac;
          }
        }
        return View();
    }

    [HttpPost]
    public IActionResult NonAnonymus(int accid,string dname,int payment,string mail){
        IOManage im = new IOManage();
         List<Account>alist = im.GetAccount();
        foreach(Account ac in alist){
            if(alist!=null){
                int UpdatedAmount = ac.Amount+payment;
                bool status = im.NewAvailbleAmount(accid,UpdatedAmount);
                if(status){
                
                    bool status23 = im.AddContributors(dname,payment,mail);
                    TempData["message"]="Thanks For Your SuccessFull Donation";
                    return RedirectToAction("Donation","Home");
                }
            }
        }
        return View();
    }

    [HttpGet]
    public IActionResult Donation(){
       
        return View();
    }

    [HttpGet]
    public IActionResult AvailableAmount(){
        IOManage im = new IOManage();
        List<Account>alist = im.GetAccount();
        foreach(Account ac in alist){
            if(alist!=null){
            ViewData["Account"]=ac;
          }
        }
       
        
        return View();
    }

    [HttpGet]
    public IActionResult ChildPage(){
        
        
        return View();
    }

    [HttpGet]
    public IActionResult ChildStatus(int chid){
         IOManage im = new IOManage();
          List<ChildSts>chlist = im.GetChild();
        foreach(ChildSts ch in chlist){
            if(chlist!=null){
                if(ch.Chid==chid){
                    ViewData["Child"]=ch;
                }
                
            }
        }
        
         return View();
    }
   
    [HttpGet]
    public IActionResult AdminIndex(){
      IOManage im = new IOManage();
      List<ChildSts> chlist = im.GetChild();
      if(chlist!=null){
          ViewData["Child"]=chlist;
      }  
      return View();
    }
    [HttpGet]
    public IActionResult LoginAdmin(string role){
        ViewData["AdminRole"]=role;
        return View();
    }
    [HttpGet]
     public IActionResult LoginChild(string role1){
        ViewData["ChildRole"]=role1;
        return View();
    }
    [HttpGet]
    public IActionResult Insert(){
        IOManage im = new IOManage();
        List<ChildSts>chlist = im.GetChild();
        foreach(ChildSts ch in chlist){
            if(chlist!=null){
                ViewData["Child"]=ch;
            }
        }
        
        return View();
    }
   
    [HttpGet]
    public IActionResult Delete(int chid){
        IOManage im=new IOManage();
        bool status=im.RemoveUser(chid);
        if(status){
            TempData["message"]="Removed SuccessFully";
            return RedirectToAction("AdminIndex","Home");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Edit([FromQuery(Name="chid")] int chid){
        IOManage im = new IOManage();
        List<ChildSts>chlist = im.GetChild();
        foreach(ChildSts ch in chlist){
            if(chlist!=null){
                if(ch.Chid==chid){
                  ViewData["Child"]=ch;
                  break;
                }
                
            }
        }
          List<Account>alist = im.GetAccount();
        foreach(Account ac in alist){
            if(alist!=null){
            ViewData["Account"]=ac;
            break ;
          }
        }

        return View();
    } 
    [HttpPost]
    public IActionResult Edit(int accid,int chid,string payment,string date){
        IOManage im = new IOManage();
        Console.WriteLine(chid);
        int payment1;
        Int32.TryParse(payment,out payment1);
        List<Account> alist = im.GetAccount();
        foreach(Account ac in alist){
             if(alist!=null){
                    if(payment1<0){
                         TempData["message"]="put proper Amount";
                        return RedirectToAction("AdminIndex","Home");
                    }
                    if(ac.Amount<payment1){
                        Console.WriteLine(ac.Amount+"<<"+payment1);
                        TempData["message"]="Insufficient Amount no changes is applied please check Available amount";
                        return RedirectToAction("AdminIndex","Home");
                        // return RedirectToAction("Edit","Home");
                    }
                    if(ac.Amount>=payment1){
                        int UpdatedAmount=ac.Amount-payment1;//sending operated Amount
                        bool status23 =im.NewAvailbleAmount(accid,UpdatedAmount);
                        string payment2 = payment1.ToString();
                        bool status = im.Update(chid,payment2,date);
                        if(status){
                        TempData["message"]="Updated Successfully";
                        return RedirectToAction("AdminIndex","Home");
                       }
                    }

             }
             break ;

              
        } 
        
        
        
        
       
        return View();
    }

    [HttpPost]
    public IActionResult Insert(string password,string chname,string date,int chid){
        IOManage im = new IOManage();
        string role="child";
        string chkey = Guid.NewGuid().ToString();
        bool status = false;

        status = im.ChildInx(chname,date,password,role,chid,chkey);

        if(status){
            TempData["message"]="Registered SuccessFully";
           return RedirectToAction("AdminIndex","Home");
        }

        return View();

    }
    
    [HttpPost]
    public IActionResult LoginAdmin(string uname,string password,string role){
        IOManage im = new IOManage();
        List<AdminUser>ulist  = im.GetAdminUser();
        string role1 ="admin";
        
        foreach(AdminUser u in ulist){
                  Console.WriteLine(u.Role+"-"+role+"-"+role1);
                //    Console.WriteLine(u.Uname+"-"+uname);

            if(u!=null){
                  
                if(u.Role.Equals(role)||u.Role.Equals(role1)){     //remeber to put chid in 1st condition otherwise loop will break
                    Console.WriteLine(u.Uname+"-"+uname);
                   if(u.Uname.Equals(uname) && u.Password.Equals(password)){
                       
                      return RedirectToAction("AdminIndex","Home");
                   }
                   TempData["message"]="Invalid Credentials";
                    return RedirectToAction("LoginAdmin","Home");
                }
                
            }
        }
        TempData["message"]="Get Register First";
        return RedirectToAction("LoginAdmin","Home");

        // return View();
    }
    [HttpPost]
    public IActionResult LoginChild(int uid,string uname,string password,string role1){
         IOManage im = new IOManage();
        List<User>ulist  = im.GetAll();
        string role2 = "child";
        foreach(User u in ulist){
            if(u!=null){
                 Console.WriteLine(u.Uid+"=="+uid);
                if(u.Uid==uid){
                    
                    Console.WriteLine(u.Role+"-"+role1);
                  if(u.Role.Equals(role1)||u.Role.Equals(role2)){
                       
                     if(u.Uname.Equals(uname) && u.Password.Equals(password)){
                        TempData["uname"]=uname;
                        TempData["uid"] = uid;
                         return RedirectToAction("ChildPage","Home");
                     }
                     TempData["message"]="sorry you are not registered as Child";
                    return RedirectToAction("LoginChild","Home");
                }
                TempData["message"]="invalid credentials";
                return RedirectToAction("LoginChild","Home");
                  }
            }

        }
            TempData["message"]="Get Register First";
        return RedirectToAction("LoginChild","Home");

        return View();
        }
       
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
