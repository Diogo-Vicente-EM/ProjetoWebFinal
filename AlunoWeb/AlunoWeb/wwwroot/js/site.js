const { data } = require("jquery");

function somenteNumeros(e) {
    var charCode = e.charCode ? e.charCode : e.keyCode;
   
    if (charCode != 8 && charCode != 9 && charCode != 13) {
       
        if ((charCode < 48 || charCode > 57) && (charCode < 95 || charCode > 105))  {
            return false;
        }
    }
}
function idade() {
    //instantaneos do objeto Date, veja explicação no final da resposta
    let dataNascimento = new Date(document.getElementById("DataNascimento").value); 
    let dataAtual = new Date();

    let dataMiliS = dataAtual - dataNascimento;
    const difAnos = dataMiliS / (1000 * 60 * 60 * 24 * 365.25);
    let difAnosInteiro=Math.floor(difAnos);
    if (difAnos > 1 && difAnos < 173) {
        return difAnosInteiro + " Ano(s) de Idade";
    }
    
    return "";
}

function chamar() {
    document.getElementById("idade").innerHTML = idade() ;
}
