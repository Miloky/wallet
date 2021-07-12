import React from 'react';
import ReactDOM from 'react-dom';
import { createMuiTheme, CssBaseline, MuiThemeProvider } from '@material-ui/core';
import { QueryClient, QueryClientProvider } from 'react-query';

import Routes from './components/routes';
import './index.scss';


// TODO: Tests
// TODO: WebVitals
// TODO: Eslint

const theme = createMuiTheme({});


const queryClient = new QueryClient();

ReactDOM.render(
  <React.StrictMode>
    <MuiThemeProvider theme={theme}>
      <CssBaseline/>
      <QueryClientProvider client={queryClient}>
        <Routes/>
      </QueryClientProvider>
    </MuiThemeProvider>
  </React.StrictMode>,
  document.getElementById('root')
);
