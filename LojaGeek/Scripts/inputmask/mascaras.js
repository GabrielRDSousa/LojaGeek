$(document).ready(function () {
    $('.date').inputmask("mask", { "mask": "00/00/0000" });
    $('.time').inputmask("mask", { "mask": "00:00:00" });
    $('.discount').inputmask("mask", { "mask": "00%" });
    $('.date_time').inputmask("mask", { "mask": "00/00/0000 00:00:00" });
    $('.cep').inputmask("mask", { "mask": "00000-000" });
    $('.phone_with_ddd').inputmask("mask", { "mask": "(00) 0000-0000" });
    $('.phone_us').inputmask("mask", { "mask": "00/00/0000" });
    $('.cpf').inputmask("mask", { "mask": "000.000.000-00" });
    $('.cnpj').inputmask("mask", { "mask": "00.000.000/0000-00" });
    $('.money').inputmask("mask", { "mask": "000.000.000.000.000,00" });
    $('.money2').inputmask("mask", { "mask": "#.##0,00" });
    $('.percent').inputmask("mask", { "mask": "##%" });

    $('.placeholder').inputmask("mask", { "mask": "00/00/0000", "placeholder": "__/__/____" });
    $('.selectonfocus').inputmask("mask", { "mask": "00/00/0000", "selectOnFocus": true });

    $('.num_cartao').inputmask("mask", { "mask": "#### #### #### ####" });
    $('.venc_cartao').inputmask("mask", { "mask": "##/####" });
    $('.cod_cartao').inputmask("mask", { "mask": "###" });
});