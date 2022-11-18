import React, { Component, useEffect } from 'react';
import PropTypes from 'prop-types';

import ControllPanelScreen from './src/screens/ControllPanelScreen';
import HomeScreen from './src/screens/HomeScreen';
import LoginScreen from './src/screens/LoginScreen';
import EditTaskScreen from './src/screens/EditTaskScreen';

import { NavigationContainer } from '@react-navigation/native'; 
import {createStackNavigator} from '@react-navigation/stack';

class Home extends React.Component {
  render() {
    const {navigate} = this.props.navigation;
      return (
        <HomeScreen navigation = {this.props.navigation}/>
      )
  }
}

class ControllPanel extends React.Component {
  render() {
    return (
        <ControllPanelScreen navigation = {this.props.navigation}/>
    );
  }
}

class Login extends React.Component {
  render() {
    return (
        <LoginScreen navigation = {this.props.navigation}/>
    );
  }
}

class EditTask extends React.Component {
  render() {
    return (
        <EditTaskScreen navigation = {this.props.navigation}/>
    );
  }
}

const MyTheme = {
  colors: {
    background:'#F8F6EA'
  },
};

const Stack  = createStackNavigator();
function Container() {
  return (
      <NavigationContainer theme={MyTheme}>
          <Stack.Navigator initialRouteName="Home" screenOptions={{ gestureEnabled: true, headerLeft: null, unmountOnBlur: true} } >
              <Stack.Screen
                name="Main"
                component={Home}
                options={{ 
                  title: 'Serwis rowerowy',
                  headerStyle: { backgroundColor: '#f4511e' },
                  headerTintColor: '#fff',
                  headerTitleAlign: 'center'                
                }}
              />
              <Stack.Screen
                name="ControllPanel"
                component={ControllPanel}   
                options={{ 
                  title: 'Panel administracyjny',
                  headerStyle: { backgroundColor: '#f4511e' },
                  headerTintColor: '#fff',
                  headerTitleAlign: 'center'                
                }}   
              />
              <Stack.Screen
                name="EditTask"
                component={EditTask}  
                options={{ 
                  title: 'Zlecenie',
                  headerStyle: { backgroundColor: '#f4511e' },
                  headerTintColor: '#fff',
                  headerTitleAlign: 'center'                
                }}    
              />
              <Stack.Screen
                name="Login"
                component={Login}  
                options={{ 
                  title: 'Serwis rowerowy',
                  headerStyle: { backgroundColor: '#f4511e' },
                  headerTintColor: '#fff',
                  headerTitleAlign: 'center'                
                }}    
              />
          </Stack.Navigator>
      </NavigationContainer>
  );
}
export default Container;  

Home.propTypes ={
  navigation: PropTypes.object.isRequired,
};
