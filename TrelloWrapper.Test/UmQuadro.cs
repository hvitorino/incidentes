﻿using NUnit.Framework;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class UmQuadro
    {
        private Quadro quadro;

        [OneTimeSetUp]
        public void Cenario()
        {
            quadro = new Quadro();
        }

        [Test]
        public void TemNome()
        {
            Assert.That(quadro.Nome, Is.EqualTo("Incidentes"));
        }

        [Test]
        public void TemAListaSubmitted()
        {
            Assert.That(quadro.Submitted, Is.Not.Null);
        }

        [Test]
        public void TemAListaEmInvestigacao()
        {
            Assert.That(quadro.EmInvestigacao, Is.Not.Null);
        }

        [Test]
        public void TemAListaEmResolucao()
        {
            Assert.That(quadro.EmResolucao, Is.Not.Null);
        }

        [Test]
        public void TemAListaPendencia()
        {
            Assert.That(quadro.Pendencia, Is.Not.Null);
        }

        [Test]
        public void PodeMoverCartaoEntreListas()
        {
            var cartaoEmSubmitted = new Cartao
            {
                Nome = "Cartão 1"
            };

            quadro.AdicionaCartaoA(cartaoEmSubmitted, quadro.Submitted);
            quadro.MoveCartaoPara(cartaoEmSubmitted, quadro.EmInvestigacao);

            Assert.That(quadro.EmInvestigacao.Cartoes, Contains.Item(cartaoEmSubmitted));
        }
    }
}
