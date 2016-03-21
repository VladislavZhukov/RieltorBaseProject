using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VI_EF
{
    /// <summary>
    /// Для использования тип проекта должен быть ConsoleApplication
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            using (VolgaInfoDBEntities context = new VolgaInfoDBEntities())
            {
                //var action = context.Actions.ToList();

                //foreach (var item in action)
                //{
                //    Console.WriteLine(item.Name);
                //}

                AddAction(context, "Test_Insert");
                //for (int i = 0; i < 10; i++)
                //{
                //    AddAgent(context, "Иван", "Петровчук", "Свердлова 1", "764661", 2, false);
                //    context.SaveChanges();
                //}


                //SamplingOfIfFirm(context, 3);

                //SearchBySurname(context, "Севконов", 2);    

                

            }

            Console.ReadLine();
        }

        private static void SearchBySurname(VolgaInfoDBEntities context, string lastName, int idFirm)
        {
            IEnumerable<Agent> myAgentIEnumerable = context.Agents.ToList();
            Console.WriteLine(myAgentIEnumerable);

            myAgentIEnumerable = myAgentIEnumerable.Where(ma => ma.LastName == lastName);
            Console.WriteLine(myAgentIEnumerable);
            foreach (var item in myAgentIEnumerable)
            {
                Console.WriteLine(item.Name, item.LastName, item.Addres, item.PhoneNumber);
            }
            Console.WriteLine("---------------------------------------------");
            IQueryable<Agent> myAgentIQueriable = context.Agents;
            Console.WriteLine(myAgentIQueriable);
            myAgentIQueriable = myAgentIQueriable.Where(ma => ma.LastName == lastName);
            Console.WriteLine(myAgentIQueriable);
            foreach (var item in myAgentIQueriable)
            {
                Console.WriteLine(item.Name, item.LastName, item.Addres, item.PhoneNumber);
            }
            Console.WriteLine("---------------------------------------------");
            var myAgentIQuAndIEnu = context.Agents.Where(ma => ma.LastName == lastName).ToList().Where(ma => ma.Id_firm == idFirm);
            Console.WriteLine(myAgentIQuAndIEnu);
            foreach (var item in myAgentIQuAndIEnu)
            {
                Console.WriteLine(item.Name, item.LastName, item.Addres, item.PhoneNumber);
            }

        }

        private static void SamplingOfIfFirm(VolgaInfoDBEntities context, int firmId)
        {
            var agents = context.Agents.ToList();

            var query = from myAgents in agents
                        where myAgents.Id_firm == firmId
                        select new { myAgents.Name, myAgents.LastName, myAgents.PhoneNumber };

            foreach (var item in query)
            {
                Console.WriteLine("Name: {0} Last name: {1} PhoneNumber: {2}", item.Name, item.LastName, item.PhoneNumber);
            }
        }

        private static void AddAction(VolgaInfoDBEntities context, string nameAction)
        {
            Action a1 = new Action { Name = nameAction };

            context.Actions.Add(a1);

            context.SaveChanges();

            var action1 = context.Actions.ToList();

            foreach (var item in action1)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static void AddAgent(VolgaInfoDBEntities context, string name, string lastName, string addres, string phoneNumber, int idFirm, bool firmAdmin)
        {
            Agent a1 = new Agent { Name = name, LastName = lastName, Addres = addres, PhoneNumber = phoneNumber, Id_firm = idFirm, IsFirmAdmin = firmAdmin };

            context.Agents.Add(a1);

            context.SaveChanges();

            //var agent1 = context.Agents.ToList();

            //foreach (var item in agent1)
            //{
            //    Console.WriteLine(item.Name, item.LastName);
            //}

            //удалить объект из базы
            //context.Agents.Remove(a1);
            //context.SaveChanges();
        }
    }
}
