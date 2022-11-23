using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Athenas.Service.Interfaces;
using Microsoft.Azure.Documents;
using Athenas.Domain;
using Athenas.Repository;

namespace Athenas.Service
{
    public class DoctorService : IDoctorService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Listagem de doctor
        public async Task<IEnumerable<Doctor>> ListarProfissionais(string idAdm, string idSer)
        {
            List<Doctor> profissionais = (List<Doctor>)await Repository<Doctor>.ListarProfissionais(idAdm, idSer);
            if (profissionais == null)
            {
                return null;
            }
            else
            {
                return profissionais;
            }
        }

        //Cadastrar um doctor
        public async Task<Doctor> CadastrarDoctor(string idAdm, string IdEspecialidade, Doctor pro)
        {

            Administrador adm = await Repository<Administrador>.PegarAdm(idAdm);
            pro.IdServico = idEspecialidade;

            Doctor vali = await Repository<Doctor>.PegarDoctorPorEmail2(adm, pro.Email);

            if (vali == null)
            {
                var retorno = await Repository<Doctor>.CadastrarItem(pro);

                pro = await Repository<Doctor>.PegarDoctorPorEmail(pro.Email);

                pro.Agendamento = new List<Agendamento>();

                var retorno2 = await Repository<Administrador>.CadastrarDoctor(adm, pro);

                await Repository<Servico>.DeletarItem(pro.Id);

                if (adm == null || pro == null || retorno == null || retorno2 == null)
                {
                    return null;
                }
                else
                {
                    return pro;
                }

            }
            else
            {
                Console.WriteLine("O email inserido já está cadastrado!");
                return null;
            }
        }

        //Pegar um único doctor
        public async Task<Doctor> PegarDoctor(string idAdm, string id)
        {
            Doctor doctor = await Repository<Doctor>.PegarDoctor(idAdm, id);

            if (doctor == null)
            {
                throw null;
            }
            else
            {
                return doctor;
            }
        }

        //Atualizar um doctor
        public async Task<Doctor> AtualizarDoctor(string idAdm, Doctor doctor)
        {
            Administrador adm = await Repository<Administrador>.PegarAdm(idAdm);
            var retorno = await Repository<Doctor>.AtualizarDoctor(adm, doctor);

            if (doctor == null || adm == null || retorno == null)
            {
                return null;
            }
            else
            {
                return doctor;
            }
        }

        //Deletar um doctor
        public async Task DeletarDoctor(string idAdm, string id)
        {
            Doctor doctor = await Repository<Doctor>.PegarDoctor(idAdm, id);
            Administrador adm = await Repository<Administrador>.PegarAdm(idAdm);

            if (doctor == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<Doctor>.DeletarDoctor(doctor, adm);
            }
        }
    }
}
