using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.MVC.Util
{
    public static class SweetAlert
    {
        internal static string MensagemSucesso(string mensagem)
        {
            return string.Format("swal('Sucesso!', '{0}', 'success');", mensagem);
        }

        internal static string MensagemSucessoComissao(string mensagem)
        {
            return string.Concat(" swal({title: 'Sucesso!',text: '", mensagem, "', type: 'success', showCancelButton: true, confirmButtonColor: '#3085d6', cancelButtonColor: '#d33',confirmButtonText: 'Imprimir recibo',cancelButtonText: 'Ok',confirmButtonClass: 'btn btn-success',cancelButtonClass: 'btn btn-danger',buttonsStyling: false,closeOnConfirm: false,closeOnCancel: true},function (isConfirm) {if (isConfirm) {var win = window.open('/Comissao/Recibo/', '_blank');}});");
        }

        internal static object MensagemErro(string mensagem)
        {
            return string.Format("swal('Oops...', '{0}', 'error');", mensagem);
        }

        internal static object MensagemErroNaoTratado(string exception)
        {
            return string.Format("swal('Oops...', 'Algo inesperado aconteceu, tente novamente! ({0})', 'error');", exception);
        }
    }
}