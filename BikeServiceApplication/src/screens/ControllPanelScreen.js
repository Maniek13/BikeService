import React, { Component } from 'react';
import {View, Text, Button, BackHandler, StyleSheet, FlatList, TouchableOpacity} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import TasksController from '../controllers/TasksController';
import User from '../objects/User';
import Task from '../objects/Task';
import Error from '../components/Error';
import Response from '../objects/Response';

class ControllPanelScreen extends Component {
  constructor(props){
    super(props);
    this.handleBackButton = this.handleBackButton.bind(this);
    
    this.state = {
      refreshed: 0,
      showError: false,
      error: {}   
    };
  }
  
  async componentDidMount (){
    Response.response = {
      code: 0,
      data: {
        message: ''
      }
    }
    
    await TasksController.getTasks();
    this.setState({ showError: false });

    let onTime = setInterval(() => {
      if(Response.response.code !== 0){
        if(Response.response.code === 1){
          TasksController.tasksList = Response.response.data.Order;
          this.setState({ refreshed: this.state.refreshed + 1 });
        }
        else{
          this.setState({
            error: Response.response
          });
          this.setState({ showError: true });
        }
        
        clearInterval(onTime);
      }
    }, 100);

    this.focusListener = this.props.navigation.addListener('focus', () => {
      this.setState({ refreshed: this.state.refreshed + 1});
      BackHandler.addEventListener('hardwareBackPress', this.handleBackButton);
    });
  }

  componentWillUnmount() {
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
  }

  handleBackButton(){
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
    this.props.navigation.push('Main');
    return true;
  } 

  async logOut(){
    await AsyncStorage.removeItem('@BikeServiceUserId');
    await AsyncStorage.removeItem('@BikeServiceUserLogin');
    await AsyncStorage.removeItem('@BikeServiceUserPassword');
    await AsyncStorage.removeItem('@BikeServiceUserAppId');

    User.user.Id = 0;
    User.user.Name = '';
    User.user.Password = '';
    TasksController.tasksList = [];

    this.handleBackButton()
  }

  editTask(item){
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
    Task.task = item;
    this.props.navigation.navigate('EditTask')
  }

  addTask(){
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
    this.props.navigation.navigate('AddTask')
  }
  
  render() {
    return (
      <View style={styles.conteiner}>
        <Button 
          onPress={this.logOut.bind(this)}
          title="Wyloguj"
        />
        <Text style={styles.text}>Zadania</Text>
        <FlatList
          data={TasksController.tasksList}
          extraData={this.state.refreshed}
          renderItem={({item}) => 
            <TouchableOpacity style={styles.listItem} onPress={this.editTask.bind(this, item)}>
              <Text style={styles.textList, {marginLeft: 10, color: 'black'}}>{item.header}</Text>
              <Text style={styles.textList, {marginLeft: 'auto', marginRight: 10, color: 'black'}}>{String(Task.statusList.find(x => x.Value === item.state).Label)}</Text>
            </TouchableOpacity>
          }
        />
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}

        <TouchableOpacity style={styles.searchButton} onPress={this.addTask.bind(this)}>
            <Text style={styles.buttonText}>+</Text>
        </TouchableOpacity>
        
      </View>
    );
  }
}

export default ControllPanelScreen;

const styles = StyleSheet.create({
  text : {
    color: '#000000',
    textAlign: 'center',
    fontSize: 20,
    marginTop: 10,
    marginBottom: 10
  },
  textList : {
    height: 20
  },
  listItem : {
    borderWidth: 1,
    marginBottom: 5,
    marginLeft: 10,
    marginRight: 10,
    flexDirection:'row', 
    flexWrap:'wrap',
    backgroundColor: 'white'
  },
  buttonText : {
    color: 'white',
    textAlign: 'center',
    fontSize: 30,
    fontWeight: 'bold'
  },
  searchButton: {
    alignSelf: 'flex-end',
    position: 'absolute',
    justifyContent: 'center',
    backgroundColor: '#249ef0',
    bottom: 10,
    right: 10,
    width: 60,
    height: 60,
    padding: 5,
    borderRadius: 30,
    zIndex: 100
  },
  conteiner:{
    flexDirection: 'column',
    flex: 1
  }
});


