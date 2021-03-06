﻿using NHibernate.Event;
using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    internal class AuditEventListener : IPreUpdateEventListener, IPreInsertEventListener
    {

        public bool OnPreUpdate(PreUpdateEvent e)
        {
            return AtualizarMaiusculas(e.Entity);
        }

        private bool AtualizarMaiusculas(Object e) {
            //UpdateAuditTrail(e.State, e.Persister.PropertyNames, (IEntity)e.Entity);

            Object origem = e;

            Type t = origem.GetType();
            foreach (PropertyInfo piOrigem in t.GetProperties())
            {
                // Se contiver o Attribute ManterCase, não faz nada
                object[] manter = piOrigem.GetCustomAttributes(typeof(ManterCaseAttribute), false);
                // Gambiarra provisória. Somente se for Simbolo, mas haverá um Attribute para definir se deve ou não upper
                if ((piOrigem.PropertyType == typeof(string)) && (piOrigem.CanWrite) && (manter.Length == 0) )
                {
                    Type tipo = piOrigem.GetType();

                    if (piOrigem.GetValue(origem) != null)
                    {
                        piOrigem.SetValue(origem, piOrigem.GetValue(origem).ToString().ToUpperInvariant());
                    }

                }
            }
            return false; 
        
        }

        public bool OnPreInsert(PreInsertEvent e)
        {
            return AtualizarMaiusculas(e.Entity);
        }

        /*
        private void UpdateAuditTrail(object[] state, string[] names, IEntity entity)
        {
    
            var idx = Array.FindIndex(names, n => n == "UpdatedBy");
            state[idx] = string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name) ? "Unknown" : Thread.CurrentPrincipal.Identity.Name;
            entity.UpdatedBy = state[idx].ToString();
            idx = Array.FindIndex(names, n => n == "UpdatedTimestamp");
            DateTime now = DateTime.Now;
            state[idx] = now;
            entity.UpdatedTimestamp = now;
        }
        */
        /*
        bool IPreInsertEventListener.OnPreInsert(PreInsertEvent @event)
        {
            throw new NotImplementedException();
        }
         */ 
    }
}
