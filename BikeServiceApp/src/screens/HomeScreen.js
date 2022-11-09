import React, { Component } from 'react';
import {View, Text, Button} from 'react-native';
import LoginScreen from '../screens/LoginScreen';
import UsersTasksScreen from '../screens/UsersTasksScreen';

class HomeScreen extends Component {
  constructor(props){
    super(props);
  }

  state = {
          loginPage: false,
          userTask: false
      };
  
  render() {
    return (
      <View>
        <Button
          onPress={() => {
            this.setState({ loginPage: true });
            this.setState({ userTask: false });
          }}
          title="Log in"
        />
        <Button 
          onPress={() => {
            this.setState({ userTask: true });
            this.setState({ loginPage: false });
          }} 
          title="ForUsser"
        />
        {this.state.loginPage === true ? <LoginScreen navigate = {this.props.navigate}/> : ""}
        {this.state.userTask === true ? <UsersTasksScreen navigate = {this.props.navigate}/> : ""}
      </View>
    );
  }
}


export default HomeScreen;




