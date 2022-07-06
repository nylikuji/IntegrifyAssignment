const signupurl = 'api/signup';

function signUp() {

    const signupemail = document.getElementById('signup-email').value.trim();
    const signuppassword = document.getElementById('signup-password').value.trim();

    if (signupemail.length > 0 && signuppassword.length > 0) {
        fetch(signupurl + "?email=" + signupemail + "&password=" + signuppassword, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .then(() => {
                document.getElementById('signup-email').value = "";
                document.getElementById('signup-password').value = "";
            })
            .catch(error => console.error('Unable to add item.', error));
    }
}
