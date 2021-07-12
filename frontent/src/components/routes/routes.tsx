import React, { FunctionComponent, useCallback, useEffect, useState } from 'react';
import { Redirect, Route, Switch, useParams } from 'react-router-dom';
import AccountList from '../pages/account-list';
import authenticationService, { AuthenticationResultStatus } from '../../serivces/auth-service';
import { Profile } from 'oidc-client';
import { BrowserRouter as Router } from 'react-router-dom';
import { CircularProgress } from '@material-ui/core';
import AppFrame from '../app-frame';


const AccountDetails: FunctionComponent = () => {
  const params = useParams<{ id: string }>();

  return <div>{params.id}</div>;
};

const QueryParameterNames = {
  ReturnUrl: 'return_url'
};

const getReturnUrl = (state: any) => {
  const params = new URLSearchParams(window.location.search);
  const fromQuery = params.get(QueryParameterNames.ReturnUrl);
  if (fromQuery && !fromQuery.startsWith(`${window.location.origin}/`)) {
    // This is an extra check to prevent open redirects.
    throw new Error('Invalid return url. The return url needs to have the same origin as the current page.');
  }
  return (state && state.returnUrl) || fromQuery || `${window.location.origin}/`;
};

const LoginCallback: FunctionComponent = () => {
  const navigateToReturnUrl = useCallback(returnUrl => {
    // It's important that we do a replace here so that we remove the callback uri with the
    // fragment containing the tokens from the browser history.
    window.location.replace(returnUrl);
  }, []);

  const processLoginCallback = useCallback(async (): Promise<void> => {
    const url = window.location.href;
    const result = await authenticationService.completeSignIn(url);
    if (result.status === AuthenticationResultStatus.success) {
      navigateToReturnUrl('/' /*getReturnUrl(result.state)*/);
    }
  }, [navigateToReturnUrl]);

  useEffect(() => {
    processLoginCallback();
  }, [processLoginCallback]);

  return <>Authorizing....</>;
};


const useUser = () => {
  const [user, setUser] = useState<{ user: Profile | undefined, loading: boolean }>({loading: true, user: undefined});

  const getUser = useCallback(async () => {
    setUser({loading: true, user: undefined});
    const user = await authenticationService.getUser();
    setUser({loading: false, user});
  }, [setUser]);

  useEffect(() => {
    getUser();
  }, [getUser]);

  return user;
};

const Dashboard: FunctionComponent = () => {
  const {loading, user} = useUser();
  return <div>
    {loading ? 'Loading ...' : user?.name}
  </div>;
};

const ProtectedRoute = (props:any)=> {
  const [state, setState] = useState({ ready: false, authenticated: false });

  const checkAuthentication = useCallback(async ()=>{
    const authenticated = await authenticationService.isAuthenticated();
    setState({ ready: true, authenticated });
  }, [setState]);

  useEffect(()=>{
    checkAuthentication();
  },[checkAuthentication]);

  if(state.ready){
    if(state.authenticated){
      return  <Route {...props} />;
    } else{
      return <Redirect to='/login' />;
    }
  }

  return <div />;
};

const Login = () => {

  useEffect(()=>{
    authenticationService.signIn();
  },[]);

  return <div style={{
    position:'absolute',
    transform: 'translate(-50%, -50%)',
    left:'50%', top:'50%',
    width:'100%',
    textAlign:'center'
  }}>
    <CircularProgress />
    <div>Process authentication...</div>
  </div>;
};

const Routes: FunctionComponent = () => {
  return <Router>
    <Switch>
      <ProtectedRoute path='/' exact>
        <AppFrame>
          <Dashboard />
        </AppFrame>
      </ProtectedRoute>
      <ProtectedRoute path='/companies' exact>
        <AppFrame>
          Companies
        </AppFrame>
      </ProtectedRoute>
      <Route path='/contacts' exact>
        Contacts
      </Route>
      <Route path='/accounts' exact>
        <AccountList/>
      </Route>
      <Route path='/accounts/:id' exact>
        <AccountDetails/>
      </Route>
      <Route path='/login-callback'>
        <LoginCallback />
      </Route>
      <Route path='/login'>
        <Login />
      </Route>
      <Route path='*'>
        <div>Not found</div>
        {/*<Redirect to='/'/>*/}
      </Route>
    </Switch>
  </Router>;
};

export default Routes;
