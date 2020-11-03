using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories.Context;
using Repositories.Entities;
using Repositories.Intefaces;
using System;
using System.Collections.Generic;
using UnitTests.Mocks;
using Weather.Business.Base;

namespace UnitTests.Tests
{
    [TestClass]
    public class OpenWeatherTests
    {
        private readonly OpenWeatherMock _openWeatherDAO = new OpenWeatherMock();

        [TestMethod]
        public void NewOpenWeather()
        {
            OpenWeatherBusiness openWeatherBusiness = new OpenWeatherBusiness(_openWeatherDAO);
            var response = openWeatherBusiness.Request("Sao Paulo", DateTime.Now, DateTime.Now.AddDays(1));

            Assert.IsNotNull(response, "Resposta de retorno inválida");

            openWeatherBusiness.Execute(response).Wait();

            var list = _openWeatherDAO.ListASync().Result;
            var item = list.Find(x => x.Id == 1);

            Assert.IsTrue(list.Count > 0, "Dados não inseridos");
            Assert.IsTrue(item != null, "Id inserido inválido");
            Assert.IsTrue(list.Count == 2, "Quantidade total incorreta");

            response = openWeatherBusiness.Request("Florianopolis", DateTime.Now, DateTime.Now.AddDays(2));

            Assert.IsNotNull(response, "Resposta de retorno inválida");

            openWeatherBusiness.Execute(response).Wait();

            list = _openWeatherDAO.ListASync().Result;
            item = list.Find(x => x.Id == 3);

            Assert.IsTrue(item != null, "Id inserido inválido");
            Assert.IsTrue(list.Count == 5, "Quantidade total incorreta");

            response = openWeatherBusiness.Request("Rio de Janeiro", DateTime.Now, DateTime.Now);

            Assert.IsNotNull(response, "Resposta de retorno inválida");

            openWeatherBusiness.Execute(response).Wait();

            list = _openWeatherDAO.ListASync().Result;
            item = list.Find(x => x.Id == 6);

            Assert.IsTrue(item != null, "Id inserido inválido");
            Assert.IsTrue(list.Count == 6, "Quantidade total incorreta");
        }

        [TestMethod]
        public void CheckDates()
        {
            OpenWeatherBusiness openWeatherBusiness = new OpenWeatherBusiness(_openWeatherDAO);

            string mensagem = string.Empty;
            try
            {
                var response = openWeatherBusiness.Request("Sao Paulo", DateTime.Now, DateTime.Now.AddDays(-1));
            }
            catch (Exception e)
            {
                mensagem = e.Message;
            }

            Assert.IsTrue(mensagem.Equals("Data inicial não pode ser maior que a final"), "Não validou corretamente as datas");
        }

        [TestMethod]
        public void CheckCity()
        {
            OpenWeatherBusiness openWeatherBusiness = new OpenWeatherBusiness(_openWeatherDAO);
            
            string mensagem = string.Empty;
            try
            {
                var response = openWeatherBusiness.Request("Blumenau", DateTime.Now, DateTime.Now.AddDays(1));
            }
            catch (Exception e)
            {
                mensagem = e.Message;
            }

            Assert.IsTrue(mensagem.Equals("Cidade não compatível com a lista de cidades"), "Não validou corretamente a cidade");

            mensagem = string.Empty;
            try
            {
                var response = openWeatherBusiness.Request(string.Empty, DateTime.Now, DateTime.Now.AddDays(1));
            }
            catch (Exception e)
            {
                mensagem = e.Message;
            }

            Assert.IsTrue(mensagem.Equals("Cidade não informada"), "Não validou corretamente a cidade");
        }
    }
}
