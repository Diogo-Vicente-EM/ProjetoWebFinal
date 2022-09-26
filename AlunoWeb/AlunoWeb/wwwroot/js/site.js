const { data } = require("jquery");

function somenteNumeros(e) {
    var charCode = e.charCode ? e.charCode : e.keyCode;
   
    if (charCode != 8 && charCode != 9 && charCode != 13) {
       
        if ((charCode < 48 || charCode > 57) && (charCode < 95 || charCode > 105))  {
            return false;
        }
    }
}

$(document).ready(function () {
    $("#AlunoNome").css("background-color", "blue").show("fast");
});
function idade() {
    //instantaneos do objeto Date, veja explicação no final da resposta
    let dataNascimento = new Date(document.getElementById("DataNascimento").value); 
    let dataAtual = new Date();
    let difAno = dataAtual.getFullYear() - dataNascimento.getFullYear();


    let dataMiliS = dataAtual - dataNascimento;
    const difAnos = dataMiliS / (1000 * 60 * 60 * 24 * 365.25);
    let difAnosInteiro=Math.floor(difAnos);
    if (difAnos >= 0 && difAnos < 173) {
        return difAnosInteiro + " Ano(s) de Idade";
    }
    
    return "";
}
$("div.form-group").css("background-color", "blue").show("fast");
$(".btn btn-primary btn btn-secondary").css("background-color", "red").show("slow");
function chamar() {
    document.getElementById("idade").innerHTML = idade();
}
function displayTime() {
    var elt = document.getElementById("idade"); // Localiza o elemento com id="clock"
    var now = new Date(); // Obtém a hora atual
    elt.innerHTML = now.toLocaleTimeString(); // Faz elt exibi-la
    setTimeout(displayTime, 1000); // Executa novamente em 1 segundo
}

