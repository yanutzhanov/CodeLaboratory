let degrees = 360;
document.querySelector('#settings-btn button').addEventListener('click', (e) => {
    e.preventDefault();
    console.log("Working");
    let img = document.querySelector('#settings-btn button img');
    img.style.transform = "rotate(" + degrees + "deg)";
    img.style.transition = "1s";
    degrees += 360;
    ReplaceInfo();
});

let counter = 1;
const inputNames = ['avatar', 'login', 'email', 'github', 'firstName', 'secondName', 'age', 'city', 'discord'];

function ReplaceInfo() {
    let items = document.getElementsByClassName('item-value');
    for (let item of items) {
        setTimeout(() => {
            if (item === items[0]) { return true; }
            let input = document.createElement('input');
            if (item.parentElement.hasAttribute('number')) {
                input.setAttribute('type', 'number');
                input.setAttribute('value', parseInt(item.innerHTML));
            } else {
                input.setAttribute('type', 'text');
                input.setAttribute('value', item.innerHTML);
            }
            input.setAttribute('name', inputNames[counter]);
            counter++;
            item.replaceWith(input);
        }, 100);
    }
    counter = 1;
    if (document.querySelectorAll('input[type=submit]').length < 2) {
        let form = document.forms[1];
        let submit = document.createElement('input');
        submit.setAttribute('type', 'submit');
        submit.setAttribute('value', 'Сохранить');
        form.append(submit);
    }
    
}
