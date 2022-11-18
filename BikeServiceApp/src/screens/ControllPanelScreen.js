import React, { Component } from 'react';
import {View, Text, Button, BackHandler, StyleSheet, FlatList, TouchableOpacity} from 'react-native';
import TasksController from '../controllers/TasksController';

import AsyncStorage from '@react-native-async-storage/async-storage';

import User from '../objects/User'
import Task from '../objects/Task'

class ControllPanelScreen extends Component {
  constructor(props){
    super(props);
    this.handleBackButton = this.handleBackButton.bind(this);

    this.state = {
      taskList: []
    };
  }
  
  componentDidMount (){
    BackHandler.addEventListener('hardwareBackPress', this.handleBackButton);
    this.setState({ taskList: TasksController.getTasks()});
  }

  componentWillUnmount() {
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
  }

  handleBackButton(){
    this.props.navigate('Main');
    return true;
  } 

  async logOut(){
    await AsyncStorage.removeItem('@BikeServiceUser');

    User.user.Id = 0;
    this.props.navigate('Main');
  }

  editTask(){
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
    this.props.navigate('EditTask');
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
          data={this.state.taskList}
          renderItem={({item}) => 
            <TouchableOpacity style={styles.list} onPress={this.editTask.bind(this)}>
              <Text style={styles.textList, {marginLeft: 10}}>{item.Header}</Text>
              <Text style={styles.textList, {marginLeft: 'auto', marginRight: 10}}>{String(Task.statusList.find(x => x.Id === item.State).State)}</Text>
            </TouchableOpacity>
          }
        />

        <TouchableOpacity style={styles.searchButton}>
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
    color: 'black',
    height: 20
  },
  list : {
    borderWidth: 1,
    marginBottom: 5,
    marginLeft: 10,
    marginRight: 10,
    flexDirection:'row', 
    flexWrap:'wrap'
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


