const { data } = require("jquery");

function somenteNumeros(e) {
    var charCode = e.charCode ? e.charCode : e.keyCode;
   
    if (charCode != 8 && charCode != 9) {
       
        var max = 9;
        var num = document.getElementById('Matricula').style.display = "block";

        if ((charCode < 48 || charCode > 57) || (num.value.length >= max))  {
            return false;
        }
    }
}


function idade() {
    //instantaneos do objeto Date, veja explicação no final da resposta
    let dataNascimento = new Date(document.getElementById("DataNascimento").value); 
    let dataAtual = new Date();


    let dataMiliS = dataAtual - dataNascimento;
    const difAnos = dataMiliS / (1000 * 60 * 60 * 24 * 365);
    let difAnosInteiro=Math.floor(difAnos);
    if (difAnos > 1 && difAnos < 149) {
        return difAnosInteiro + " Ano(s) de Idade";
    }
    /*var utc2 = new Date.UTC(dataUm.getFullYear(), dataUm.getMonth(), dataUm.getDate());*/
    
    /*const diffInDays = dataFinal / (1000 * 60 * 60 * 24);*/
    
    return "";
}

function chamar() {
    document.getElementById("idade").innerHTML = idade() ;
}


//function idade() {
//    //instantaneos do objeto Date, veja explicação no final da resposta
//    let dataUm = new Date(document.getElementById("DataNascimento").value).getFullYear();
//    let dataDois = new Date().getFullYear();


//    let dataFinal = dataDois - dataUm;
//    if (dataFinal > 0 && dataFinal < 149) {
//        return dataFinal + " Anos de Idade";
//    }
//    /*var utc2 = new Date.UTC(dataUm.getFullYear(), dataUm.getMonth(), dataUm.getDate());*/

//    /*const diffInDays = dataFinal / (1000 * 60 * 60 * 24);*/

//    return "";
//}