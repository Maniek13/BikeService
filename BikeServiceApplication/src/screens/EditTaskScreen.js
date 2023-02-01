import React, {Component} from 'react';
import {View, StyleSheet, Text, TextInput, TouchableOpacity} from 'react-native';
import ModalSelector from 'react-native-modal-selector';

import TasksController from '../controllers/TasksController';
import Task from '../objects/Task';
import Error from '../components/Error';
import Settings from '../objects/Settings';
import Response from '../objects/Response';

import MainStyles from '../styles/MainStyles';
import EditTaskScreenStyles from '../styles/EditTaskScreenStyles';

class EditTaskScreen extends Component {
  constructor(props){
    super(props);
    this.state = {
      id: Task.task.TaskId,
      header: Task.task.Header,
      description: Task.task.Description,
      state: Task.task.State,
      selectItemList: [],
      showError: false,
      btnLoginDisabled: false,
      error: {}
    };
  }

  componentDidMount(){
    Task.statusList.forEach(element => {
      let item = {
        key: element.Value, 
        label: element.Label
      };
      this.setState(prevState => ({
        selectItemList: [...prevState.selectItemList, item]
      }))
    });
  }

  onEndLoading(statment){
    if(statment === "if"){
      let index = TasksController.tasksList.findIndex((obj => obj.TaskId == Task.task.TaskId));
      TasksController.tasksList[index].Header = this.state.header;
      TasksController.tasksList[index].Description = this.state.description;
      TasksController.tasksList[index].State = this.state.state;
      this.props.navigation.navigate('ControllPanel');
    }

    if(statment === "else"){
      this.setState({
        error: Response.response
      });
      this.setState({ showError: true });
      this.setState({ btnLoginDisabled: false});
    }
  }

  async update(){
    this.setState({ showError: false });
    this.setState({ btnLoginDisabled: true});

    let task = {
      TaskId: this.state.id,
      Header: this.state.header,
      Description: this.state.description,
      State: this.state.state,
    };
    Task.task = task;

    Response.response = {
      code: 0,
      data: {
        message: ''
      }
    }
    
    await TasksController.updateTask();
    Response.getDate(this.onEndLoading.bind(this));
  }

  render() {
    return (
      <View >
        <TextInput 
          value={this.state.header}
          style={mainStyle.taskHeader}
          placeholder="tytuł" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({header: newText})}
        />
        <TextInput 
          value={this.state.description}
          multiline={true}
          style={mainStyle.taskDescription}
          placeholder="opis" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({description: newText})}
          
        />
        <ModalSelector
          style={editTaskScreenStyle.select}
          data={this.state.selectItemList}
          onChange={(option)=>this.setState({state: option.key})}
          initValue={String(Task.statusList.find(x => x.Value === this.state.state).Label)}
          selectStyle={{ borderColor: "black" }}
        />
        <TouchableOpacity style={this.state.btnLoginDisabled ? mainStyle.buttonDisabled : mainStyle.buttonEnabled } onPress={this.update.bind(this)}>
          <Text style={mainStyle.buttonText}>Aktualizuj</Text>
        </TouchableOpacity>
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}
      </View>
    );
  }
}

export default EditTaskScreen;

const mainStyle = MainStyles[Settings.SchemaStyle]
const editTaskScreenStyle = EditTaskScreenStyles[Settings.SchemaStyle]

