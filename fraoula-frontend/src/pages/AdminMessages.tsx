import React, { useState } from 'react';
import {
  Container,
  Grid,
  Paper,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Avatar,
  Typography,
  Box,
  TextField,
  IconButton,
  Button,
  Chip,
  Badge,
  Divider,
  InputAdornment,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  FormControl,
  InputLabel,
  Select,
  MenuItem
} from '@mui/material';
import {
  Send,
  Search,
  AttachFile,
  Add,
  PriorityHigh,
  Reply,
  MarkAsUnread,
  Delete,
  Circle
} from '@mui/icons-material';

const AdminMessages = () => {
  const [selectedChat, setSelectedChat] = useState<any>(null);
  const [newMessage, setNewMessage] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  const [newChatOpen, setNewChatOpen] = useState(false);
  
  const [conversations, setConversations] = useState([
    {
      id: "1",
      userId: "user1",
      userName: "Ahmet Yılmaz",
      userAvatar: "/avatars/ahmet.jpg",
      lastMessage: "Yoga programı hakkında sorum var",
      lastMessageTime: "10:30",
      unreadCount: 2,
      isOnline: true,
      messageType: "Support",
      priority: "Normal",
      messages: [
        {
          id: "m1",
          senderId: "user1",
          senderName: "Ahmet Yılmaz",
          content: "Merhaba, yoga programına kayıt olmak istiyorum",
          timestamp: "10:25",
          isAdmin: false
        },
        {
          id: "m2", 
          senderId: "user1",
          senderName: "Ahmet Yılmaz",
          content: "Hangi seviyeden başlamam gerekir?",
          timestamp: "10:30",
          isAdmin: false
        }
      ]
    },
    {
      id: "2",
      userId: "user2", 
      userName: "Zeynep Kaya",
      userAvatar: "/avatars/zeynep.jpg",
      lastMessage: "Sipariş durumum nasıl?",
      lastMessageTime: "09:45",
      unreadCount: 1,
      isOnline: false,
      messageType: "Order",
      priority: "High",
      messages: [
        {
          id: "m3",
          senderId: "user2",
          senderName: "Zeynep Kaya", 
          content: "Merhaba, dün verdiğim siparişin durumunu öğrenebilir miyim?",
          timestamp: "09:45",
          isAdmin: false
        },
        {
          id: "m4",
          senderId: "admin",
          senderName: "Admin",
          content: "Merhaba Zeynep, siparişiniz hazırlanıyor. Yarın kargoya verilecek.",
          timestamp: "10:15",
          isAdmin: true
        }
      ]
    },
    {
      id: "3",
      userId: "user3",
      userName: "Can Demir", 
      userAvatar: "/avatars/can.jpg",
      lastMessage: "Teşekkürler, çok yardımcı oldunuz!",
      lastMessageTime: "Dün",
      unreadCount: 0,
      isOnline: true,
      messageType: "General",
      priority: "Low",
      messages: [
        {
          id: "m5",
          senderId: "user3",
          senderName: "Can Demir",
          content: "HIIT programı çok etkili oldu, teşekkürler!",
          timestamp: "Dün 16:30",
          isAdmin: false
        },
        {
          id: "m6",
          senderId: "admin", 
          senderName: "Admin",
          content: "Rica ederim Can! Başarınız bizim için çok değerli.",
          timestamp: "Dün 17:00",
          isAdmin: true
        }
      ]
    }
  ]);

  const [newChatData, setNewChatData] = useState({
    userName: '',
    messageType: 'General',
    priority: 'Normal',
    subject: '',
    content: ''
  });

  const messageTypes = [
    { value: 'General', label: 'Genel', color: '#2196F3' },
    { value: 'Support', label: 'Destek', color: '#FF9800' },
    { value: 'Order', label: 'Sipariş', color: '#4CAF50' },
    { value: 'Complaint', label: 'Şikayet', color: '#F44336' },
    { value: 'Program', label: 'Program', color: '#9C27B0' }
  ];

  const priorities = [
    { value: 'Low', label: 'Düşük', color: '#4CAF50' },
    { value: 'Normal', label: 'Normal', color: '#2196F3' },
    { value: 'High', label: 'Yüksek', color: '#FF9800' },
    { value: 'Urgent', label: 'Acil', color: '#F44336' }
  ];

  const getTypeColor = (type: string) => {
    return messageTypes.find(t => t.value === type)?.color || '#9E9E9E';
  };

  const getPriorityColor = (priority: string) => {
    return priorities.find(p => p.value === priority)?.color || '#9E9E9E';
  };

  const sendMessage = () => {
    if (!newMessage.trim() || !selectedChat) return;

    const message = {
      id: `m${Date.now()}`,
      senderId: "admin",
      senderName: "Admin",
      content: newMessage,
      timestamp: new Date().toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
      isAdmin: true
    };

    setConversations(conversations.map(conv => 
      conv.id === selectedChat.id 
        ? { 
            ...conv, 
            messages: [...conv.messages, message],
            lastMessage: newMessage,
            lastMessageTime: message.timestamp
          }
        : conv
    ));

    setSelectedChat({
      ...selectedChat,
      messages: [...selectedChat.messages, message]
    });

    setNewMessage('');
  };

  const createNewChat = () => {
    const newConv = {
      id: `conv${Date.now()}`,
      userId: `user${Date.now()}`,
      userName: newChatData.userName,
      userAvatar: "/avatars/default.jpg",
      lastMessage: newChatData.content,
      lastMessageTime: new Date().toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
      unreadCount: 1,
      isOnline: false,
      messageType: newChatData.messageType,
      priority: newChatData.priority,
      messages: [
        {
          id: `m${Date.now()}`,
          senderId: `user${Date.now()}`,
          senderName: newChatData.userName,
          content: newChatData.content,
          timestamp: new Date().toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
          isAdmin: false
        }
      ]
    };

    setConversations([newConv, ...conversations]);
    setNewChatOpen(false);
    setNewChatData({
      userName: '',
      messageType: 'General',
      priority: 'Normal', 
      subject: '',
      content: ''
    });
  };

  const filteredConversations = conversations.filter(conv =>
    conv.userName.toLowerCase().includes(searchTerm.toLowerCase()) ||
    conv.lastMessage.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      <Typography variant="h4" sx={{ mb: 4, fontWeight: 700, color: '#FF6B35' }}>
        💬 Müşteri Mesajları
      </Typography>

      <Grid container spacing={3} sx={{ height: 'calc(100vh - 200px)' }}>
        {/* Chat List */}
        <Grid xs={12} md={4}>
          <Paper sx={{ height: '100%', display: 'flex', flexDirection: 'column', borderRadius: 3 }}>
            {/* Search & New Chat */}
            <Box sx={{ p: 2, borderBottom: '1px solid #E0E0E0' }}>
              <TextField
                fullWidth
                placeholder="Konuşma ara..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                InputProps={{
                  startAdornment: (
                    <InputAdornment position="start">
                      <Search sx={{ color: '#FF6B35' }} />
                    </InputAdornment>
                  )
                }}
                sx={{ mb: 2 }}
              />
              <Button
                fullWidth
                variant="contained"
                startIcon={<Add />}
                onClick={() => setNewChatOpen(true)}
                sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
              >
                Yeni Konuşma
              </Button>
            </Box>

            {/* Conversations */}
            <List sx={{ flex: 1, overflow: 'auto', p: 0 }}>
              {filteredConversations.map((conv) => (
                <ListItem
                  key={conv.id}
                  onClick={() => setSelectedChat(conv)}
                  sx={{
                    borderBottom: '1px solid #F5F5F5',
                    bgcolor: selectedChat?.id === conv.id ? '#FFF8F0' : 'transparent',
                    cursor: 'pointer'
                  }}
                >
                  <ListItemAvatar>
                    <Badge 
                      variant="dot" 
                      color={conv.isOnline ? "success" : "default"}
                      anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                    >
                      <Avatar src={conv.userAvatar} alt={conv.userName}>
                        {conv.userName.charAt(0)}
                      </Avatar>
                    </Badge>
                  </ListItemAvatar>
                  <ListItemText
                    primary={
                      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <Typography variant="subtitle2">{conv.userName}</Typography>
                        <Box sx={{ display: 'flex', alignItems: 'center', gap: 0.5 }}>
                          {conv.unreadCount > 0 && (
                            <Badge badgeContent={conv.unreadCount} color="error" />
                          )}
                          <Typography variant="caption" color="text.secondary">
                            {conv.lastMessageTime}
                          </Typography>
                        </Box>
                      </Box>
                    }
                    secondary={
                      <Box>
                        <Typography variant="body2" color="text.secondary" noWrap>
                          {conv.lastMessage}
                        </Typography>
                        <Box sx={{ display: 'flex', gap: 1, mt: 0.5 }}>
                          <Chip 
                            label={messageTypes.find(t => t.value === conv.messageType)?.label}
                            size="small"
                            sx={{ 
                              bgcolor: getTypeColor(conv.messageType), 
                              color: 'white',
                              fontSize: '0.7rem'
                            }}
                          />
                          <Chip 
                            label={priorities.find(p => p.value === conv.priority)?.label}
                            size="small"
                            sx={{ 
                              bgcolor: getPriorityColor(conv.priority), 
                              color: 'white',
                              fontSize: '0.7rem'
                            }}
                          />
                        </Box>
                      </Box>
                    }
                  />
                </ListItem>
              ))}
            </List>
          </Paper>
        </Grid>

        {/* Chat Window */}
        <Grid xs={12} md={8}>
          <Paper sx={{ height: '100%', display: 'flex', flexDirection: 'column', borderRadius: 3 }}>
            {selectedChat ? (
              <>
                {/* Chat Header */}
                <Box sx={{ 
                  p: 2, 
                  borderBottom: '1px solid #E0E0E0',
                  display: 'flex',
                  justifyContent: 'space-between',
                  alignItems: 'center'
                }}>
                  <Box sx={{ display: 'flex', alignItems: 'center' }}>
                    <Badge 
                      variant="dot" 
                      color={selectedChat.isOnline ? "success" : "default"}
                      anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                    >
                      <Avatar src={selectedChat.userAvatar} sx={{ mr: 2 }}>
                        {selectedChat.userName.charAt(0)}
                      </Avatar>
                    </Badge>
                    <Box>
                      <Typography variant="h6">{selectedChat.userName}</Typography>
                      <Typography variant="caption" color="text.secondary">
                        {selectedChat.isOnline ? 'Çevrimiçi' : 'Çevrimdışı'}
                      </Typography>
                    </Box>
                  </Box>
                  <Box sx={{ display: 'flex', gap: 1 }}>
                    <IconButton size="small">
                      <MarkAsUnread />
                    </IconButton>
                    <IconButton size="small" color="error">
                      <Delete />
                    </IconButton>
                  </Box>
                </Box>

                {/* Messages */}
                <Box sx={{ flex: 1, overflow: 'auto', p: 2 }}>
                  {selectedChat.messages.map((message: any) => (
                    <Box
                      key={message.id}
                      sx={{
                        display: 'flex',
                        justifyContent: message.isAdmin ? 'flex-end' : 'flex-start',
                        mb: 2
                      }}
                    >
                      <Paper
                        sx={{
                          p: 2,
                          maxWidth: '70%',
                          bgcolor: message.isAdmin ? '#FF6B35' : '#F5F5F5',
                          color: message.isAdmin ? 'white' : 'inherit',
                          borderRadius: message.isAdmin ? '20px 20px 5px 20px' : '20px 20px 20px 5px'
                        }}
                      >
                        <Typography variant="body2" sx={{ mb: 0.5 }}>
                          {message.content}
                        </Typography>
                        <Typography 
                          variant="caption" 
                          sx={{ 
                            opacity: 0.8,
                            display: 'block',
                            textAlign: 'right'
                          }}
                        >
                          {message.timestamp}
                        </Typography>
                      </Paper>
                    </Box>
                  ))}
                </Box>

                {/* Message Input */}
                <Box sx={{ p: 2, borderTop: '1px solid #E0E0E0' }}>
                  <Box sx={{ display: 'flex', gap: 1 }}>
                    <TextField
                      fullWidth
                      placeholder="Mesajınızı yazın..."
                      value={newMessage}
                      onChange={(e) => setNewMessage(e.target.value)}
                      onKeyPress={(e) => e.key === 'Enter' && sendMessage()}
                      multiline
                      maxRows={3}
                    />
                    <IconButton sx={{ color: '#FF6B35' }}>
                      <AttachFile />
                    </IconButton>
                    <IconButton 
                      onClick={sendMessage}
                      sx={{ 
                        bgcolor: '#FF6B35', 
                        color: 'white',
                        '&:hover': { bgcolor: '#E55A2B' }
                      }}
                    >
                      <Send />
                    </IconButton>
                  </Box>
                </Box>
              </>
            ) : (
              <Box sx={{ 
                display: 'flex', 
                alignItems: 'center', 
                justifyContent: 'center', 
                height: '100%',
                flexDirection: 'column'
              }}>
                <Typography variant="h6" color="text.secondary" gutterBottom>
                  Bir konuşma seçin
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Müşterilerle mesajlaşmak için sol taraftan bir konuşma seçin
                </Typography>
              </Box>
            )}
          </Paper>
        </Grid>
      </Grid>

      {/* New Chat Dialog */}
      <Dialog open={newChatOpen} onClose={() => setNewChatOpen(false)} maxWidth="sm" fullWidth>
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          Yeni Konuşma Başlat
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          <Grid container spacing={3} sx={{ mt: 1 }}>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Müşteri Adı"
                value={newChatData.userName}
                onChange={(e) => setNewChatData({...newChatData, userName: e.target.value})}
              />
            </Grid>
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Mesaj Türü</InputLabel>
                <Select
                  value={newChatData.messageType}
                  onChange={(e) => setNewChatData({...newChatData, messageType: e.target.value})}
                  label="Mesaj Türü"
                >
                  {messageTypes.map((type) => (
                    <MenuItem key={type.value} value={type.value}>
                      <Chip 
                        label={type.label} 
                        size="small"
                        sx={{ bgcolor: type.color, color: 'white' }}
                      />
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Öncelik</InputLabel>
                <Select
                  value={newChatData.priority}
                  onChange={(e) => setNewChatData({...newChatData, priority: e.target.value})}
                  label="Öncelik"
                >
                  {priorities.map((priority) => (
                    <MenuItem key={priority.value} value={priority.value}>
                      <Chip 
                        label={priority.label} 
                        size="small"
                        sx={{ bgcolor: priority.color, color: 'white' }}
                      />
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Konu"
                value={newChatData.subject}
                onChange={(e) => setNewChatData({...newChatData, subject: e.target.value})}
              />
            </Grid>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Mesaj İçeriği"
                multiline
                rows={4}
                value={newChatData.content}
                onChange={(e) => setNewChatData({...newChatData, content: e.target.value})}
              />
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button onClick={() => setNewChatOpen(false)}>İptal</Button>
          <Button 
            variant="contained" 
            onClick={createNewChat}
            sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
          >
            Konuşma Başlat
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default AdminMessages;
