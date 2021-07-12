import React, { FunctionComponent, ReactElement } from 'react';
import {
  AppBar,
  Toolbar,
  Drawer,
  makeStyles,
  Theme,
  Divider,
  Typography,
  List,
  ListItem,
  ListItemText,
  ListItemIcon,
  Container
} from '@material-ui/core';
import { NavLink } from 'react-router-dom';

import ContactMailIcon from '@material-ui/icons/ContactMail';
import AccountBalanceIcon from '@material-ui/icons/AccountBalance';
import DashboardIcon from '@material-ui/icons/Dashboard';
import AccountBalanceWalletRoundedIcon from '@material-ui/icons/AccountBalanceWalletRounded';


const useStyles = makeStyles((theme: Theme) => ({
  root: {
    display: 'flex',
    backgroundColor: theme.palette.background.default
  },
  header: {
    zIndex: 2001
  },
  drawer: {
    width: '240px'
  },
  toolbar: theme.mixins.toolbar
}));

const items = [
  {
    title: 'Dashboard',
    icon: <DashboardIcon/>,
    link: '/'
  },
  {
    title: 'Companies',
    icon: <AccountBalanceIcon/>,
    link: '/companies'
  },
  {
    title: 'Contacts',
    icon: <ContactMailIcon/>,
    link: '/contacts'
  },
  {
    title: 'Accounts',
    icon: <AccountBalanceWalletRoundedIcon/>,
    link: '/accounts'
  }
];

const AppFrame: FunctionComponent = (props): ReactElement => {
  const classes = useStyles();

  return <div className={classes.root}>
    <AppBar color='default' position='fixed' className={classes.header}>
      <Toolbar>
        <Typography>
          Wallet
        </Typography>
      </Toolbar>
    </AppBar>
    <nav aria-label='Main navigation' style={{flexShrink: 0, width: '250px', display: 'block'}}>
      <Drawer open variant='persistent' anchor='left'>
        <div className={classes.toolbar}/>
        <Divider/>
        <div style={{width: '250px'}}/>
        <List component='nav'>
          {items.map(item => <ListItem key={item.title} button component={NavLink} to={item.link}>
            <ListItemIcon children={item.icon}/>
            <ListItemText>
              {item.title}
            </ListItemText>
          </ListItem>)}
        </List>
      </Drawer>
    </nav>
    <div style={{flexShrink: 0, width: 'calc(100% - 250px)'}}>
      <Container component='main' style={{paddingTop: '96px', paddingLeft: '48px', paddingRight: '48px'}}>
        <div>
          {props.children}
        </div>
      </Container>
    </div>
  </div>;
};

export default AppFrame;
