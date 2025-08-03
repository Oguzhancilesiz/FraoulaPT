import React, { useState } from 'react';
import {
  Box,
  Paper,
  Typography,
  IconButton,
  TextField,
  Button,
  Avatar,
  Fab,
  Slide,
  Badge,
  Chip,
  Divider
} from '@mui/material';
import {
  Chat,
  Close,
  Send,
  SupportAgent,
  Circle,
  EmojiEmotions,
  AttachFile,
  ExpandMore,
  ExpandLess
} from '@mui/icons-material';

const LiveChat = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [isMinimized, setIsMinimized] = useState(false);
  const [newMessage, setNewMessage] = useState('');
  const [hasUnread, setHasUnread] = useState(true);
  
  const [messages, setMessages] = useState([
    {
      id: 1,
      text: "Merhaba! Fraoula Fitness'a hoş geldiniz! 👋",
      isBot: true,
      timestamp: "14:30",
      avatar: "/avatars/fraoula.jpg"
    },
    {
      id: 2,
      text: "Size nasıl yardımcı olabilirim? Fitness programları, ürünler veya genel sorularınız için buradayım! 💪",
      isBot: true,
      timestamp: "14:30",
      avatar: "/avatars/fraoula.jpg"
    }
  ]);

  const quickReplies = [
    "Programa nasıl başlarım?",
    "Fiyatlar nedir?",
    "Ürün bilgisi istiyorum",
    "Kargo durumu"
  ];

  const sendMessage = () => {
    if (!newMessage.trim()) return;

    const userMessage = {
      id: messages.length + 1,
      text: newMessage,
      isBot: false,
      timestamp: new Date().toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
      avatar: "/avatars/user.jpg"
    };

    setMessages([...messages, userMessage]);
    setNewMessage('');

    // Simulate bot response
    setTimeout(() => {
      const botMessage = {
        id: messages.length + 2,
        text: "Mesajınız alındı! Kısa süre içinde size detaylı yanıt vereceğim. 🚀",
        isBot: true,
        timestamp: new Date().toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
        avatar: "/avatars/fraoula.jpg"
      };
      setMessages(prev => [...prev, botMessage]);
    }, 1500);
  };

  const handleQuickReply = (reply: string) => {
    setNewMessage(reply);
  };

  const toggleChat = () => {
    setIsOpen(!isOpen);
    if (!isOpen) {
      setHasUnread(false);
    }
  };

  return (
    <>
      {/* Chat Fab Button */}
      <Fab
        color="primary"
        onClick={toggleChat}
        sx={{
          position: 'fixed',
          bottom: 20,
          right: 20,
          bgcolor: '#FF6B35',
          '&:hover': { bgcolor: '#E55A2B' },
          zIndex: 1300,
          boxShadow: '0 8px 25px rgba(255, 107, 53, 0.3)'
        }}
      >
        <Badge
          badgeContent={hasUnread ? "!" : 0}
          color="error"
          sx={{ '& .MuiBadge-badge': { fontSize: '0.8rem', fontWeight: 'bold' } }}
        >
          <Chat />
        </Badge>
      </Fab>

      {/* Chat Window */}
      <Slide direction="up" in={isOpen} mountOnEnter unmountOnExit>
        <Paper
          sx={{
            position: 'fixed',
            bottom: 90,
            right: 20,
            width: { xs: 'calc(100vw - 40px)', sm: 380 },
            height: isMinimized ? 80 : 500,
            zIndex: 1300,
            borderRadius: 3,
            overflow: 'hidden',
            boxShadow: '0 12px 40px rgba(0,0,0,0.15)',
            display: 'flex',
            flexDirection: 'column',
            transition: 'height 0.3s ease'
          }}
        >
          {/* Chat Header */}
          <Box sx={{
            bgcolor: '#FF6B35',
            color: 'white',
            p: 2,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'space-between'
          }}>
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
              <Badge
                overlap="circular"
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                badgeContent={
                  <Circle sx={{ fontSize: 12, color: '#4CAF50' }} />
                }
              >
                <Avatar 
                          src="/avatars/fraoula.jpg"
        alt="Fraoula"
                  sx={{ width: 40, height: 40, mr: 2 }}
                />
              </Badge>
              <Box>
                <Typography variant="subtitle1" sx={{ fontWeight: 600 }}>
                  Fraoula 🔥
                </Typography>
                <Typography variant="caption" sx={{ opacity: 0.9 }}>
                  Çevrimiçi • Genellikle hemen yanıtlar
                </Typography>
              </Box>
            </Box>
            
            <Box>
              <IconButton
                size="small"
                onClick={() => setIsMinimized(!isMinimized)}
                sx={{ color: 'white', mr: 1 }}
              >
                {isMinimized ? <ExpandLess /> : <ExpandMore />}
              </IconButton>
              <IconButton
                size="small"
                onClick={toggleChat}
                sx={{ color: 'white' }}
              >
                <Close />
              </IconButton>
            </Box>
          </Box>

          {!isMinimized && (
            <>
              {/* Messages Area */}
              <Box sx={{
                flex: 1,
                overflow: 'auto',
                p: 2,
                bgcolor: '#F8F9FA'
              }}>
                {messages.map((message) => (
                  <Box
                    key={message.id}
                    sx={{
                      display: 'flex',
                      justifyContent: message.isBot ? 'flex-start' : 'flex-end',
                      mb: 2
                    }}
                  >
                    <Box sx={{
                      display: 'flex',
                      alignItems: 'flex-end',
                      maxWidth: '80%',
                      flexDirection: message.isBot ? 'row' : 'row-reverse'
                    }}>
                      <Avatar
                        src={message.avatar}
                        sx={{
                          width: 32,
                          height: 32,
                          mx: 1,
                          border: message.isBot ? '2px solid #FF6B35' : '2px solid #E0E0E0'
                        }}
                      />
                      <Paper sx={{
                        p: 2,
                        bgcolor: message.isBot ? 'white' : '#FF6B35',
                        color: message.isBot ? 'inherit' : 'white',
                        borderRadius: message.isBot ? '18px 18px 18px 4px' : '18px 18px 4px 18px',
                        boxShadow: '0 2px 8px rgba(0,0,0,0.1)'
                      }}>
                        <Typography variant="body2">
                          {message.text}
                        </Typography>
                        <Typography
                          variant="caption"
                          sx={{
                            display: 'block',
                            textAlign: 'right',
                            mt: 0.5,
                            opacity: 0.7
                          }}
                        >
                          {message.timestamp}
                        </Typography>
                      </Paper>
                    </Box>
                  </Box>
                ))}

                {/* Quick Replies */}
                {messages.length <= 2 && (
                  <Box sx={{ mt: 2 }}>
                    <Typography variant="caption" color="text.secondary" sx={{ mb: 1, display: 'block' }}>
                      Hızlı yanıtlar:
                    </Typography>
                    <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 1 }}>
                      {quickReplies.map((reply, index) => (
                        <Chip
                          key={index}
                          label={reply}
                          size="small"
                          onClick={() => handleQuickReply(reply)}
                          sx={{
                            bgcolor: 'white',
                            border: '1px solid #FF6B35',
                            color: '#FF6B35',
                            '&:hover': {
                              bgcolor: '#FF6B35',
                              color: 'white'
                            },
                            cursor: 'pointer'
                          }}
                        />
                      ))}
                    </Box>
                  </Box>
                )}
              </Box>

              <Divider />

              {/* Message Input */}
              <Box sx={{ p: 2, bgcolor: 'white' }}>
                <Box sx={{ display: 'flex', gap: 1, alignItems: 'flex-end' }}>
                  <TextField
                    fullWidth
                    placeholder="Mesajınızı yazın..."
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                    onKeyPress={(e) => e.key === 'Enter' && sendMessage()}
                    multiline
                    maxRows={3}
                    size="small"
                    sx={{
                      '& .MuiOutlinedInput-root': {
                        borderRadius: '20px',
                        '&.Mui-focused fieldset': {
                          borderColor: '#FF6B35'
                        }
                      }
                    }}
                  />
                  
                  <IconButton size="small" sx={{ color: '#9E9E9E' }}>
                    <EmojiEmotions />
                  </IconButton>
                  
                  <IconButton size="small" sx={{ color: '#9E9E9E' }}>
                    <AttachFile />
                  </IconButton>
                  
                  <IconButton
                    onClick={sendMessage}
                    disabled={!newMessage.trim()}
                    sx={{
                      bgcolor: '#FF6B35',
                      color: 'white',
                      '&:hover': { bgcolor: '#E55A2B' },
                      '&.Mui-disabled': { bgcolor: '#E0E0E0' }
                    }}
                  >
                    <Send />
                  </IconButton>
                </Box>
                
                <Typography variant="caption" color="text.secondary" sx={{ mt: 1, display: 'block' }}>
                  Fraoula genellikle birkaç dakika içinde yanıtlar ⚡
                </Typography>
              </Box>
            </>
          )}
        </Paper>
      </Slide>
    </>
  );
};

export default LiveChat;
