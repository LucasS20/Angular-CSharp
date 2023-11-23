using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Exceptions;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Paginacao;

namespace ProEventos.Application;

public class EventService : IEventService
{
    private readonly IEventPersist _eventPersist;
    private readonly IMapper _autoMapper;

    public EventService(IEventPersist eventPersist, IMapper autoMapper)
    {
        _eventPersist = eventPersist;
        _autoMapper = autoMapper;
    }

    public async Task<EventDto> Add(EventDto dto)
    {
        ValidaLotes(dto);
        ValidaPreco(dto);
        var evento = _autoMapper.Map<Event>(dto);
        _eventPersist.Add(evento);
        if (await _eventPersist.SaveChangesAsync())
        {
            var retorno = await _eventPersist.GetEventByIdAsync(evento.Id);
            return _autoMapper.Map<EventDto>(retorno);
        }

        return null;
    }


    public async Task<EventDto> Update(EventDto model, int id)
    {
        ValidaLotes(model);
        ValidaPreco(model);
        var evento = await _eventPersist.GetEventByIdAsync(id);
        if (evento == null)
        {
            return null;
        }

        var _event = _autoMapper.Map(model, evento);

        _event.Batches = _autoMapper.Map<IEnumerable<Batch>>(model.Batches);


        _eventPersist.Update(_event);
        if (await _eventPersist.SaveChangesAsync())
        {
            return model;
        }


        return null;
    }


    public async Task<bool> Delete(int id)
    {
        var evento = await _eventPersist.GetEventByIdAsync(id);
        if (evento == null) throw new Exception("Event with this id not found");
        _eventPersist.Delete(evento);
        return await _eventPersist.SaveChangesAsync();
    }


    public async Task<PageList<EventDto>> GetAllEventsAsync(PageParams pageParams)
    {
        try
        {
            var events = await _eventPersist.GetAllEventsAsync(pageParams);
            var result = _autoMapper.Map<PageList<EventDto>>(events);
            result.CurrentPage = events.CurrentPage;
            result.TotalPages = events.TotalPages;
            result.TotalCount = events.TotalCount;
            result.PageSize = events.PageSize;
            
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EventDto> GetEventByIdAsync(int eventoId, bool includeSpeaker = false)
    {
        try
        {
            var evento = await _eventPersist.GetEventByIdAsync(eventoId, includeSpeaker);
            if (evento == null) return null;
            var result = _autoMapper.Map<EventDto>(evento);

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    private bool DataFinalAtualMenorQueDataInicialProximo(BatchDto loteAtual, BatchDto proximoLote)
    {
        return loteAtual.EndDate < proximoLote.StartDate;
    }

    private static bool DataInicialMenorQueFinal(BatchDto batch)
    {
        return batch.StartDate < batch.EndDate;
    }

    private void ValidaLotes(EventDto evento)
    {
        var listaDeLotes = evento.Batches.ToList();
        listaDeLotes = DefineHorasParaMeiaNoite(listaDeLotes);
        listaDeLotes[^1].EndDate = evento.Date;
        for (var i = 0; i < listaDeLotes.Count - 1; i++)
        {
            var loteAtual = listaDeLotes[i];
            var proximoLote = listaDeLotes[i + 1];
            if (!DataInicialMenorQueFinal(listaDeLotes[i]))
                throw new LoteDataInvalidaException(
                    $"A data de inicio do lote {loteAtual.Name} é maior do que a data de fim");
            if (!DataFinalAtualMenorQueDataInicialProximo(loteAtual, proximoLote))
            {
                throw new LoteDataInvalidaException(
                    $"A data final do lote {i + 1} é maior que a data inicial do lote {i + 2}.");
            }
        }

        if (!DataInicialMenorQueFinal(listaDeLotes[^1]))
        {
            throw new LoteDataInvalidaException("A data inicial do último lote é maior ou igual à data final.");
        }
    }

    private void ValidaPreco(EventDto eventModel)
    {
        var listaLotes = eventModel.Batches.ToList();
        for (var i = 0; i < listaLotes.Count - 1; i++)
        {
            var precoAtual = listaLotes[i].Price;
            var proximoPreco = listaLotes[i + 1].Price;
            if (!(precoAtual < proximoPreco))
            {
                throw new PrecoInvalidoException(
                    $"O preço do lote {i + 1} tem que ser menor  do que o do lote {i + 2}.");
            }
        }
    }

    private static List<BatchDto> DefineHorasParaMeiaNoite(List<BatchDto> listaDeLotes)
    {
        foreach (var lote in listaDeLotes)
        {
            lote.StartDate = lote.StartDate.Date;
            lote.EndDate = lote.EndDate.Date.AddMinutes(1);
        }

        return listaDeLotes;
    }
}