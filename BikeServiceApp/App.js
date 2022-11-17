import React, { Component, useEffect } from 'react';
import { View, Text, Button} from 'react-native';
import PropTypes from 'prop-types';

import ControllPanelScreen from './src/screens/ControllPanelScreen';
import HomeScreen from './src/screens/HomeScreen';
import LoginScreen from './src/screens/LoginScreen';

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
        <ControllPanelScreen navigate = {this.props.navigation.navigate}/>
    );
  }
}

class Login extends React.Component {
  render() {
    return (
        <LoginScreen navigate = {this.props.navigation.navigate}/>
    );
  }
}

const Stack  = createStackNavigator();
function Container() {
  return (
      <NavigationContainer>
          <Stack.Navigator initialRouteName="Home" screenOptions={{ gestureEnabled: true, headerLeft: null} } >
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
                name="Login"
                component={Login}  
                options={{ 
                  title: 'Logowanie',
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
