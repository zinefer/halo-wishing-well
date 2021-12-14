import React from "react";
import { useMsal } from "@azure/msal-react";
import Button from "react-bootstrap/Button";

import { authRequest } from "../Config";

function handleLogin(instance) {
    instance.loginRedirect(authRequest).catch(e => {
        console.error(e);
    });
}

/**
 * Renders a button which, when selected, will redirect the page to the login prompt
 */
export const SignInButton = () => {
    const { instance } = useMsal();

    return (
        <Button variant="secondary" className="ml-auto" onClick={() => handleLogin(instance)}>Sign in using Redirect</Button>
    );
}