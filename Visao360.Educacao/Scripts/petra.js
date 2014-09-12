// FUNÇÕES GLOBAIS
function carregarCidades(objUF, objCidade) {
    //alert('petra.js -> carregarCidades');
    var uf_id = objUF.val() || 0;
    var cidade_id = objCidade.val();
    $.getJSON("/Home/GetListaCidade", { ufId: uf_id },
       function (lista) {
           var options = '';
           $.each(lista, function (i, item) {
               var selected = item.Id == cidade_id ? " selected='selected' " : "";
               options += "<option value='" + item.Id + "'" + selected + ">"
                       + item.Descricao + "</option>";
           });
           objCidade.removeAttr('disabled').html(options);
       });
}

$(function() {
    $('input[type="text"]').setMask();
    //alert('script petra carregado');

    $(".data").datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior'
    });

});
