import React from "react";
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";

import './App.css';

import { SignInButton } from "./components/SignInButton";
import { CoinCounter } from "./components/CoinCounter";
import { Coin } from './components/Coin'
import { authConfig } from "./Config";

const AppWrapper = () =>
  <div className="App">
    <Coin />
    <CoinCounter />
  </div>

function App() {
  if (authConfig.disable) {
    return <AppWrapper />
  } else {
    return (
      <>
        <AuthenticatedTemplate>
          <AppWrapper />
        </AuthenticatedTemplate>
        <UnauthenticatedTemplate>
          <p>You are not signed in! Please sign in.</p>
          <br />
          <SignInButton />
        </UnauthenticatedTemplate>
      </>
    )
  }
}

export default App;
