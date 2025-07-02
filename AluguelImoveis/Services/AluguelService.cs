using AluguelImoveis.Models;
using AluguelImoveis.Models.DTOs;
using AluguelImoveis.Repositories.Interfaces;
using AluguelImoveis.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelImoveis.Services
{
    public class AluguelService : IAluguelService
    {
        private readonly IAluguelRepository _aluguelRepository;
        private readonly IImovelRepository _imovelRepository;
        private readonly ILocatarioRepository _locatarioRepository;

        public AluguelService(
            IAluguelRepository aluguelRepository,
            IImovelRepository imovelRepository,
            ILocatarioRepository locatarioRepository
        )
        {
            _aluguelRepository = aluguelRepository;
            _imovelRepository = imovelRepository;
            _locatarioRepository = locatarioRepository;
        }

        public async Task<Aluguel> CreateAsync(Aluguel aluguel)
        {
            // Validação de datas
            if (aluguel.DataTermino <= aluguel.DataInicio)
            {
                throw new InvalidOperationException(
                    "Data de término deve ser posterior à data de início."
                );
            }

            // Verifica se já há aluguéis com conflito de datas
            var alugueisExistentes = await _aluguelRepository.GetByImovelAsync(
                aluguel.ImovelId
            );

            bool existeConflito = alugueisExistentes.Any(
                a => a.DataTermino >= aluguel.DataInicio && a.DataInicio <= aluguel.DataTermino
            );

            if (existeConflito)
            {
                throw new InvalidOperationException(
                    "O imóvel selecionado já está alugado nesse período."
                );
            }

            // Verifica se o imóvel está disponível
            var imovel = await _imovelRepository.GetByIdAsync(aluguel.ImovelId);
            if (imovel == null)
            {
                throw new InvalidOperationException(
                    "Imóvel não encontrado ou não está disponível."
                );
            }

            if (!imovel.Disponivel)
            {
                throw new InvalidOperationException("O imóvel não está disponível para aluguel.");
            }

            // Se o aluguel for para hoje ou para o futuro, marca o imóvel como indisponível
            if (aluguel.DataTermino >= DateTime.Today)
            {
                imovel.Disponivel = false;
                await _imovelRepository.UpdateAsync(imovel);
            }

            return await _aluguelRepository.CreateAsync(aluguel);
        }

        public async Task DeleteAsync(Guid id)
        {
            var aluguel = await _aluguelRepository.GetByIdAsync(id);

            if (aluguel == null)
                throw new Exception("Aluguel não encontrado.");

            var imovel = await _imovelRepository.GetByIdAsync(aluguel.ImovelId);
            if (imovel != null)
            {
                imovel.Disponivel = true;
                await _imovelRepository.UpdateAsync(imovel);
            }

            await _aluguelRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Aluguel>> GetAllAsync()
        {
            return await _aluguelRepository.GetAllAsync();
        }

        public async Task<Aluguel> GetByIdAsync(Guid id)
        {
            return await _aluguelRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Aluguel aluguel)
        {
            await _aluguelRepository.UpdateAsync(aluguel);
        }

        public async Task<List<AluguelDto>> GetAllDetailedAsync()
        {
            var alugueis = await _aluguelRepository.GetAllDetailedAsync();
            //DEV ver se retorno os ids tambem
            return alugueis
                .Select(
                    a =>
                        new AluguelDto
                        {
                            AluguelId = a.Id,
                            DataInicio = a.DataInicio,
                            DataTermino = a.DataTermino,
                            TotalDias = (a.DataTermino - a.DataInicio).Days,
                            DiasRestantes = (a.DataTermino - DateTime.Now).Days,
                            Imovel = new ImovelDto
                            {
                                Endereco = a.Imovel.Endereco,
                                Tipo = a.Imovel.Tipo,
                                Disponivel = a.Imovel.Disponivel,
                                Codigo = a.Imovel.Codigo,
                                ValorLocacao = a.Imovel.ValorLocacao
                            },
                            Locatario = new LocatarioDto
                            {
                                NomeCompleto = a.Locatario.NomeCompleto,
                                CPF = a.Locatario.CPF,
                                Telefone = a.Locatario.Telefone
                            }
                        }
                )
                .ToList();
        }
    }
}
