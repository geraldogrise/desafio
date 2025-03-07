using AutoMapper;
using MediatR;
using FluentValidation;
using Carrefour.Desafio.Domain.Repositories;
using Carrefour.Desafio.Domain.Entities;

namespace Carrefour.Desafio.Application.Lancamentos.UpdateLancamento
{

    /// <summary>
    /// Handler for processing UpdateLancamentoCommand requests.
    /// </summary>
    public class UpdateLamcamentoHandler : IRequestHandler<UpdateLancamentoCommand, UpdateLancamentoResult>
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of UpdateLancamentoHandler.
        /// </summary>
        public UpdateLamcamentoHandler(ILancamentoRepository lancamentoRepository, IMapper mapper)
        {
            _lancamentoRepository = lancamentoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the UpdateLancamentoCommand request.
        /// </summary>
        public async Task<UpdateLancamentoResult> Handle(UpdateLancamentoCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateLancamentoCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingLancamento = await _lancamentoRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingLancamento == null)
                throw new InvalidOperationException($"Lancamento with ID {command.Id} not found");

            // Atualiza os valores do usuário
            _mapper.Map(command, existingLancamento);

            var updatedLancamento = await _lancamentoRepository.UpdateAsync(command.Id, existingLancamento, cancellationToken);
            return _mapper.Map<UpdateLancamentoResult>(updatedLancamento);
        }
    }
}
