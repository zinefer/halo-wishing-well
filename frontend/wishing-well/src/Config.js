export const authConfig = {
    auth: {
        clientId: process.env.REACT_APP_AD_APPLICATION_ID,
        authority: "https://login.microsoftonline.com/" + process.env.REACT_APP_AD_TENANT_ID, // This is a URL (e.g. https://login.microsoftonline.com/{your tenant ID})
        redirectUri: process.env.REACT_APP_AD_REDIRECT,
    },
    cache: {
        cacheLocation: "sessionStorage", // This configures where your cache will be stored
        storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
    },
    disable: process.env.REACT_APP_DONOTUSE_ONLY_FOR_CI_DISABLE_AUTHENTICATION
};

// Add scopes here for ID token to be used at Microsoft identity platform endpoints.
export const authRequest = {
    scopes: ["User.Read"]
};

export const wishingWellApiConfig = {
    endpoint: "http://localhost:5000"
};