using System;
using System.Collections;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Intercept;
using NHibernate.Persister.Entity;
using System.Reflection;

namespace Dardani.EDU.Entities.Model
{
    public class EntityUpper : ISaveOrUpdateEventListener
    {
        public EntityUpper()
        {
        }

        void ISaveOrUpdateEventListener.OnSaveOrUpdate(SaveOrUpdateEvent @event)
        {
            //@event.Session;
            //@event.Entity;
            //@event.EntityName;
            //throw new NotImplementedException();

            ConversorUpper.Converter(@event.Entity);


            //Console.WriteLine();
        }
    }

    class ConversorUpper
    {
        public static void Converter(Object origem)
        {
            Type t = origem.GetType();
            foreach (PropertyInfo piOrigem in t.GetProperties())
            {
                if (piOrigem.GetType().Equals( typeof(String) )) {

                    Type tipo = piOrigem.GetType();

                    if (piOrigem.GetValue(origem) != null) {
                        piOrigem.SetValue(origem, piOrigem.GetValue(origem).ToString().ToUpperInvariant());
                    }
                
                }
            }
        }

        /*
        private static void SetarPropriedade(Object objetoAlvo, string campo, Object valor)
        {
            Type tipo = objetoAlvo.GetType();
            PropertyInfo piDestino = tipo.GetProperty(campo);

            if ((objetoAlvo != null) && (piDestino != null))
            {
                piDestino.SetValue(objetoAlvo, valor);
            }
        }
         */ 


    }

}