using HashCode2022;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace OnePizza
{
    class Program
    {
        static void Main(string[] args)
        {
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var thisAppPath = appPathMatcher.Match(exePath).Value;

            string outputPath = Path.Combine(thisAppPath, "OutputFiles");
            //string PathToFile = Path.Combine(thisAppPath, "InputFiles\\a_an_example.in.txt");
            //string PathToFile = Path.Combine(thisAppPath, "InputFiles\\b_better_start_small.in.txt");
            string PathToFile = Path.Combine(thisAppPath, "InputFiles\\c_collaboration.in.txt");
            //string PathToFile = Path.Combine(thisAppPath, "InputFiles\\d_dense_schedule.in.txt");
            //string PathToFile = Path.Combine(thisAppPath, "InputFiles\\e_exceptional_skills.in.txt");
            //string PathToFile = Path.Combine(thisAppPath, "InputFiles\\f_find_great_mentors.in.txt");


            int ContNumber;
            int ProjectNumber;
            HashSet<Skill> AllSkills = new HashSet<Skill>();
            var ContrS = new List<Contributor>();
            var ProjS = new List<Project>();


            using (StreamReader sr = new StreamReader(PathToFile))
            {
                var line = sr.ReadLine();
                var Parts = line.Split(' ');
                ContNumber = int.Parse(Parts[0]);
                ProjectNumber = int.Parse(Parts[1]);
               


                for (int k = 0; k < ContNumber; k++)
                {

                    line = sr.ReadLine();
                    Parts = line.Split(' ');
                    var ContName = Parts[0];
                    int ContSkillNumber = int.Parse(Parts[1]);
                    var newContributer = new Contributor();
                    newContributer.Name = ContName;
                    newContributer.Skills = new List<Skill>();

                    for (int i = 0; i < ContSkillNumber; i++)
                    {
                        line = sr.ReadLine();
                        Parts = line.Split(' ');
                        var SkillName = Parts[0];
                        int SkillLevel = int.Parse(Parts[1]);
                        //Console.WriteLine(SkillName);
                        var newSkill = new Skill();
                        newSkill.Name = SkillName;
                        newSkill.Level = SkillLevel;

                        AllSkills.Add(newSkill);
                        newContributer.Skills.Add(newSkill);
                    }

                    ContrS.Add(newContributer);
                }

                //Console.WriteLine(SkillName);
                for (int m = 0; m < ProjectNumber; m++)
                {

                    line = sr.ReadLine();
                    Parts = line.Split(' ');
                    var ProjectName = Parts[0];
                    
                    int NumberOfDays = int.Parse(Parts[1]);
                    int Score = int.Parse(Parts[2]);
                    int BestBefore = int.Parse(Parts[3]);
                    int NumberOfRoles = int.Parse(Parts[4]);
                    //Console.WriteLine(ProjectName + NumberOfRoles.ToString());

                    var newProject = new Project();
                    newProject.Name = ProjectName;
                    newProject.NumberOfDays = NumberOfDays;
                    newProject.Score = Score;
                    newProject.NumberOfRoles = NumberOfRoles;
                    newProject.Skills = new List<Skill>();

                    for (int i = 0; i < NumberOfRoles; i++)
                    {
                        line = sr.ReadLine();
                        Parts = line.Split(' ');
                        var SkillName = Parts[0];
                        int SkillLevel = int.Parse(Parts[1]);
                        //Console.WriteLine(SkillName);
                        var newSkill = new Skill();
                        newSkill.Name = SkillName;
                        newSkill.Level = SkillLevel;

                        AllSkills.Add(newSkill);
                        newProject.Skills.Add(newSkill);
                    }

                    ProjS.Add(newProject);
                }
            }

            //////////////////////////////////////////////////////////////////
            ///
            var Solution = new List<PlanProject>();
            //var orderedProjects = ProjS;//.OrderByDescending(x => x.Score).ToList();
            int index = 0;
            while (index < 10)
            {
                index++;
                var orderedProjects = new List<Project>(ProjS);
                foreach (var project in orderedProjects)
                {
                    var pr = new PlanProject();
                    pr.project = project;
                    pr.contributors = new HashSet<Contributor>();
                    foreach (var item in project.Skills)
                    {
                        var pers = ContrS.Where(x => x.Skills.Where(y => y.Name.Equals(item.Name) & y.Level >= item.Level).Any()).FirstOrDefault();
                        //var pers = ContrS.Where(x => x.Skills.Where(y => y.Name.Equals(item.Name)).Any()).FirstOrDefault();
                        if (pers != null)
                        {
                            if (pr.contributors.Count < project.NumberOfRoles)
                                pr.contributors.Add(pers);
                        }
                    }
                    if (pr.contributors.Count == project.Skills.Count)
                    {

                        foreach (var cont in pr.contributors)
                        {
                            foreach (var sk in pr.project.Skills)
                            {

                                var hasSkill = cont.Skills.Where(x => x==sk).Any();
                                if (hasSkill==true)
                                    cont.Skills.Where(x => x.Name.Equals(sk.Name)).First().Level++;
                            }
                        }

                        Solution.Add(pr);
                        ProjS.Remove(pr.project);
                    }
                }
            }

            ////////////////////////////////////////////////////////////////////



            var pp = outputPath + "\\c.txt";
            using (StreamWriter sw = new StreamWriter(pp))
            {
                sw.WriteLine(Solution.Count);

                foreach (var item in Solution)
                {
                    sw.WriteLine(item.project.Name);
                    //sw.WriteLine();
                    foreach (var c in item.contributors)
                    {
                        sw.Write(c.Name);
                        sw.Write(' ');
                    }
                    sw.WriteLine();
                }
            }




            Console.WriteLine("Done!");

            Console.ReadKey();
        }
    }
}

