import { PublicClientApplication } from "@azure/msal-browser";

import { authConfig, authRequest, wishingWellApiConfig } from "../Config";

const instance = new PublicClientApplication(authConfig);

function requestAuthTokenThen(callback) {
    if (authConfig.disable) {
        return callback();
    }
    const activeAccount = instance.getActiveAccount();
    const accounts = instance.getAllAccounts();

    if (!activeAccount && accounts.length === 0) {
        /*
        * User is not signed in. This should not happen.
        * Do not call this service from an unprotected component.
        * Do nothing.
        */
        return;
    }

    const request = {
        ...authRequest,
        account: activeAccount || accounts[0]
    };

    // Silently acquires an access token and then pass it to callback to be used in
    // a future request
    return instance.acquireTokenSilent(request).then((response) => {
        callback(response.accessToken);
    });
}

/**
 * Query the backend API for the number of coins in the wishing-well
 */
export async function wellCoinCount() {
    return requestAuthTokenThen((accessToken) => {
        const headers = new Headers();
        const bearer = `Bearer ${accessToken}`;

        headers.append("Authorization", bearer);

        const options = {
            method: "GET",
            headers: headers
        };

        fetch(wishingWellApiConfig.endpoint + "/api/coins/count", options)
            .then(response => response.json())
            .catch(error => console.log(error));
    });
}

/**
 * Toss a coin into the wishing-well
 */
export async function tossCoinIntoWell() {
    return requestAuthTokenThen((accessToken) => {
        const headers = new Headers();
        const bearer = `Bearer ${accessToken}`;

        headers.append("Authorization", bearer);

        const options = {
            method: "POST",
            headers: headers,
            body: "{}"
        };

        fetch(wishingWellApiConfig.endpoint + "/api/coins/create", options)
            .then(response => response.json())
            .catch(error => console.log(error));
    });
}