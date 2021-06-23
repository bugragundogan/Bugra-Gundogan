var timeleft = 75;
var downloadTimer = setInterval(function(){
  if(timeleft <= 0){
    clearInterval(downloadTimer);
    document.getElementById("counter").innerHTML = "0";
  } else {
    document.getElementById("counter").innerHTML = timeleft;
  }
  timeleft -= 1;
}, 1000);

function openInNewTab(url) {
    window.open(url, '_blank').focus();
}